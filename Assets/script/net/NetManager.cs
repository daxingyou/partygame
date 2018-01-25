/*************************************************************
   Copyright(C) 2017 by dayugame
   All rights reserved.
   
   NetManager.cs
   PartyRhythmGame
   
   Created by WuIslet on 2018-01-12.
   
*************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace isletspace
{
    /// <summary>
    /// 
    /// </summary>
    public class NetManager : ISingleton<NetManager>
    {
        [Tooltip("从我开始到训练阶段开始的时间间隔(ms)")]
        public string starttimegap = "49000";

        string ipaddress = "61.174.15.157:4001";
        private SocketNetTools socketNetTools;

        private void Awake()
        {
            socketNetTools = gameObject.GetComponent<SocketNetTools>();
        }

        public void StartNet()
        {
            InitHallSocket(ipaddress);

            AddEventListener(PacketType.ConnectSucc, OnConnectOK);
            AddEventListener(PacketType.AccountCountRet, OnUpdatePlayerNum);
            AddEventListener(PacketType.MsgAck, OnAck);

            socketNetTools.OnConnect -= OnConnect;
            socketNetTools.OnConnect += OnConnect;
        }


        public void InitHallSocket(string serverAddr)
        {
            if (string.IsNullOrEmpty(serverAddr))
            {
                return;
            }

            string[] adds = serverAddr.Split(':');
            string ip = adds[0];
            int port = int.Parse(adds[1]);

            if (socketNetTools.Connected)
            {
                if (socketNetTools.address == ip && socketNetTools.port == port)
                {
                    return;
                }
            }
            
            socketNetTools.StopClient();
            socketNetTools.StartClient(ip, port);
        }

        void OnConnect()
        {
            if (!socketNetTools.Connected)
            {
                Debug.Log("连接游戏服务器失败");
            }
        }

        public void AddEventListener(PacketType cmd, System.Action<NetPacket> callback)
        {
            socketNetTools.AddEventListener((int)cmd, callback);
        }
        public void RemoveEventListener(PacketType cmd, System.Action<NetPacket> callback)
        {
            if (socketNetTools == null)
            {
                return;
            }

            socketNetTools.RemoveEventListener((int)cmd, callback);
        }

        #region Event
        public void OnConnectOK(NetPacket msg)
        {
            var timePackage = new NetPacket();
            timePackage.msg_id = PacketType.UploadStartTime;
            timePackage.data = "{\"start_time\":" + GetTimeStamp() + ",\"gap\":" + starttimegap + "}";
            socketNetTools.SendMsg(timePackage);
            Debug.Log("   ok " + msg.data);
        }

        public void OnUpdatePlayerNum(NetPacket msg)
        {
            print("    update player num  " + msg.data);
            GameManager.SetPlayNum(msg);
        }

        public void OnAck(NetPacket msg)
        {
            print("    ACK  " + msg.data);
        }
        #endregion

        static public string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }
    }
}
