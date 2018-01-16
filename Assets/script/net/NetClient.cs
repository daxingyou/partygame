using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;
using System.Reflection;
using isletspace;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class NetClient
{
	public string address = "";
    public int port = 5000;
    private Socket client;
    //用于存放接收数据
    public byte[] buffer;
    //每次接受和发送数据的大小
    private const int size = 1024;
    //接收数据池
    private List<byte> receiveCache;
    private bool isReceiving;
    // 连接成功
    public Action connectCallBack;
    //接收到消息之后的回调
    public Action<NetPacket> receiveCallBack;

    public void StartClient()
    {
        buffer = new byte[size];
        receiveCache = new List<byte>();

        IPAddress[] adds = Dns.GetHostAddresses(address);

        if (adds[0].AddressFamily == AddressFamily.InterNetworkV6)
        {
            Debug.Log("Connect InterNetworkV6");
            client = new Socket(AddressFamily.InterNetworkV6, SocketType.Stream, ProtocolType.Tcp);
        }
        else
        {
            Debug.Log("Connect InterNetwork");
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        IAsyncResult result = client.BeginConnect(adds, port, AsyncAccept2, client);
        //IAsyncResult result = client.BeginConnect(IPAddress.Parse(address), port, AsyncAccept2, client);

        DelayUtils.Start(waitConnection(result));
        Debug.Log(string.Format("StartClient {0}:{1}", address, port));
    }

    private IEnumerator waitConnection(IAsyncResult result)
    {
        WaitForSeconds wait = new WaitForSeconds(.2f);
        while (!result.IsCompleted)
        {
            yield return wait;
        }
        AsyncAccept(result);
    }

    private void AsyncAccept2(IAsyncResult reuslt)
    {
        Debug.Log("AsnycAccept2");
    }

    public void StopClient()
    {
        if (client == null)
            return;

        if (!client.Connected)
            return;

        try
        {
            client.Shutdown(SocketShutdown.Both);
            Debug.Log("Shutdown Socket");
        }
        catch
        {
        }

        try
        {
            client.Close();
            Debug.Log("Close Socket");
        }
        catch
        {
        }
    }

    public bool Connected
    {
        get
        {
            return null != client && client.Connected;
        }
    }

    public void SendMsg(NetPacket msg)
    {
        if (Connected)
        {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
            string dataStr = Utils.ToStr(msg);
            if(msg.msg_id != PacketType.HeartBeat)
            {
                Debug.LogFormat("=>>:{0} - data:[{1}]", msg.msg_id, dataStr);
            }
#endif

            string data = JsonConvert.SerializeObject(msg);

            byte[] result = Encoding.UTF8.GetBytes(data.ToCharArray());

            Debug.Log(result.Length);
            client.Send(result);
        }
        else
        {
            Debug.LogWarning("SendMsg not Connected : " + msg.msg_id);
        }
    }

    //回调函数， 有连接的时候会自动调用此方法
    private void AsyncAccept(IAsyncResult result)
    {
        if (null != connectCallBack)
        {
            connectCallBack();
        }

        try
        {
            Socket client = result.AsyncState as Socket;
            Debug.Log("AsyncAccept");
            BeginReceive(client);
        }
        catch (Exception ex)
        {
            Debug.Log(ex.ToString());
        }
    }

    //异步监听消息
    private void BeginReceive(Socket client)
    {
        try
        {
            //异步方法
            IAsyncResult result = client.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, EndReceive2, client);
            DelayUtils.Start(waitReceive(result));
        }
        catch (Exception ex)
        {
            Debug.Log(ex.ToString());
        }
    }

    private IEnumerator waitReceive(IAsyncResult result)
    {
        WaitForSeconds wait = new WaitForSeconds(.2f);
        while (!result.IsCompleted)
        {
            yield return wait;
        }
        EndReceive(result);
    }

    private void EndReceive2(IAsyncResult result)
    {
        //Debug.Log("EndReceive2");
    }

    //监听到消息之后调用的函数
    private void EndReceive(IAsyncResult result)
    {
        //try
        {
            Socket client = result.AsyncState as Socket;
            //获取消息的长度
            int len = client.EndReceive(result);
            if (len > 0)
            {
                byte[] data = new byte[len];
                Buffer.BlockCopy(buffer, 0, data, 0, len);
                //用户接受消息
                Receive(data);
                //尾递归，再次监听消息
                BeginReceive(client);
            }

        }
        //catch (Exception ex)
        //{
        //    Debug.Log(ex.ToString());
        //}
    }

    // 服务器接受客户端发送的消息
    public void Receive(byte[] data)
    {
        //UnityEngine.Debug.Log("接收到数据");
        //将接收到的数据放入数据池中
        receiveCache.AddRange(data);
        //如果没在读数据
        if (!isReceiving)
        {
            isReceiving = true;
            ReadData();
        }
    }

    // 读取数据
    private void ReadData()
    {
        string data = Encoding.UTF8.GetString(receiveCache.ToArray());

        /*
        var tmp = receiveCache.ToArray();
        Debug.Log("    Read Data list  " + tmp.Length);
        for (int i = 0; i < receiveCache.Count; ++i)
        {
            Debug.Log("  for  -- " + i + " : " + tmp[i]);
        }
        Debug.Log("    Read Data list >>>>>>>>>>>  " + data);
        */

        Debug.Log("  Readdate    " + data);
        //说明获取到一条完整数据
        if (data != null && data != "")
        {
            //清空消息池
            receiveCache.Clear();
            JsonSerializer serializer = new JsonSerializer();
            StringReader sr = new StringReader(data);
            NetPacket msg = serializer.Deserialize(new JsonTextReader(sr), typeof(NetPacket)) as NetPacket;

            
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
            string dataStr = Utils.ToStr(msg);
            Debug.LogFormat("<<=:{0} - data:[{1}]", msg.msg_id, dataStr);
#endif
            if (receiveCallBack != null)
            {
                receiveCallBack(msg);
            }
            //尾递归，继续读取数据
            ReadData();
        }
        else
        {
            isReceiving = false;
        }
    }
}
