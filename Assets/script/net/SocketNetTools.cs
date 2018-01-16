using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using isletspace;

public class SocketNetTools : MonoBehaviour
{
    [System.NonSerialized]
    public string address;
    [System.NonSerialized]
    public int port;

    private NetClient client;
    // 连接成功
    public System.Action OnConnect;
    public Queue<NetPacket> pools = new Queue<NetPacket>();
    private Dictionary<int, System.Action<NetPacket>> listeners = new Dictionary<int, System.Action<NetPacket>>();
    private Dictionary<int, System.Action<NetPacket>> onceListeners = new Dictionary<int, System.Action<NetPacket>>();
    private bool connectFinish = false;

    public void StartClient(string address,int port)
    {
        this.address = address;
        this.port = port;

        client = new NetClient();
        client.address = address;
        client.port = port;
        client.receiveCallBack -= OnReceive;
        client.receiveCallBack += OnReceive;
        client.connectCallBack -= connectCallBack;
        client.connectCallBack += connectCallBack;
        client.StartClient();
        connectFinish = false;
    }

    public void StopClient()
    {
        if (null != client)
        {
            client.receiveCallBack -= OnReceive;
            client.connectCallBack -= connectCallBack;
            client.StopClient();
        }
    }

    public bool Connected
    {
        get
        {
            return null != client && client.Connected;
        }
    }

    void connectCallBack()
    {
        connectFinish = true;
    }

    void Update()
    {
        if (connectFinish)
        {
            connectFinish = false;
            if(null != OnConnect)
            {
                OnConnect();
            }
        }

        if (pools.Count > 0)
        {
            NetPacket msg = pools.Dequeue();
            if (null != msg)
            {
                DispatchEvent((int)msg.msg_id, msg);
                DispatchOnceEvent((int)msg.msg_id, msg);
            }
        }
    }

    public void SendMsg(NetPacket msg)
    {
        client.SendMsg(msg);
    }

    public void SendMsg(NetPacket msg, PacketType cmd, System.Action<NetPacket> callback)
    {
        AddEventOnceListener((int)cmd, callback);
        client.SendMsg(msg);
    }

    void OnDestroy()
    {
        StopClient();
    }

    void OnReceive(NetPacket msg)
    {
        pools.Enqueue(msg);
    }

#region Event
    public void AddEventListener(int type, System.Action<NetPacket> handler)
    {
        if (handler == null)
            return;

        if (listeners.ContainsKey(type))
        {
            //这里涉及到Dispath过程中反注册问题，必须使用listeners[type]+=..
            listeners[type] += handler;
        }
        else
        {
            listeners.Add(type, handler);
        }
    }

    private void AddEventOnceListener(int type, System.Action<NetPacket> handler)
    {
        if (handler == null)
            return;

        if (onceListeners.ContainsKey(type))
        {
            //这里涉及到Dispath过程中反注册问题，必须使用listeners[type]+=..
            onceListeners[type] += handler;
        }
        else
        {
            onceListeners.Add(type, handler);
        }
    }

    public void RemoveEventListener(int type, System.Action<NetPacket> handler)
    {
        if (handler == null)
            return;

        if (listeners.ContainsKey(type))
        {
            //这里涉及到Dispath过程中反注册问题，必须使用listeners[type]-=..
            listeners[type] -= handler;
            if (listeners[type] == null)
            {
                //已经没有监听者了，移除.
                listeners.Remove(type);
            }
        }
    }

    private void RemoveEventOnceListener(int type, System.Action<NetPacket> handler)
    {
        if (handler == null)
            return;

        if (onceListeners.ContainsKey(type))
        {
            //这里涉及到Dispath过程中反注册问题，必须使用listeners[type]-=..
            onceListeners[type] -= handler;
            if (onceListeners[type] == null)
            {
                //已经没有监听者了，移除.
                onceListeners.Remove(type);
            }
        }
    }

    private static readonly string szErrorMessage = "NetworkManager Error, Event:{0}, Error:{1}, {2}";

    public void DispatchEvent(int evt, NetPacket msg)
    {
        try
        {
            if (listeners.ContainsKey(evt))
            {
                System.Action<NetPacket> handler = listeners[evt];
                if (handler != null)
                    handler(msg);
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError(string.Format(szErrorMessage, evt, ex.Message, ex.StackTrace));
        }
    }

    private void DispatchOnceEvent(int evt, NetPacket msg)
    {
        try
        {
            if (onceListeners.ContainsKey(evt))
            {
                System.Action<NetPacket> handler = onceListeners[evt];
                if (handler != null)
                {
                    handler(msg);
                    RemoveEventOnceListener(evt, handler);
                }
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError(string.Format(szErrorMessage, evt, ex.Message, ex.StackTrace));
        }
    }

    public void ClearAll()
    {
        listeners.Clear();
    }

    public void ClearEvents(int key)
    {
        if (listeners.ContainsKey(key))
        {
            listeners.Remove(key);
        }
    }
#endregion
}
