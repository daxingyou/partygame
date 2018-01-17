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
    public class NetManager : MonoBehaviour
    {
        string ipaddress = "192.168.0.77:4001";

        public SocketNetTools SocketNetTools;
        public List<RankPanel> rankPanels;

        public void StartNet()
        {
            InitHallSocket(ipaddress);

            AddEventListener(PacketType.ConnectSucc, OnConnectOK);
            AddEventListener(PacketType.AccountCountRet, OnUpdatePlayerNum);
            AddEventListener(PacketType.RankListRet, OnRankListRet);
            AddEventListener(PacketType.MsgAck, OnAck);

            SocketNetTools.OnConnect -= OnConnect;
            SocketNetTools.OnConnect += OnConnect;
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

            if (SocketNetTools.Connected)
            {
                if (SocketNetTools.address == ip && SocketNetTools.port == port)
                {
                    return;
                }
            }
            
            SocketNetTools.StopClient();
            SocketNetTools.StartClient(ip, port);
        }

        void OnConnect()
        {
            if (!SocketNetTools.Connected)
            {
                Debug.Log("连接游戏服务器失败");
            }
        }

        public void AddEventListener(PacketType cmd, System.Action<NetPacket> callback)
        {
            SocketNetTools.AddEventListener((int)cmd, callback);
        }

        #region Event
        public void OnRankListRet(NetPacket msg)
        {
            Debug.Log("   OnRankListRet ");
            for (int i = 0; i < rankPanels.Count; ++i)
            {
                rankPanels[i].OnRankRet(msg);
            }
        }

        public void OnConnectOK(NetPacket msg)
        {
            var timePackage = new NetPacket();
            timePackage.msg_id = PacketType.UploadStartTime;
            timePackage.data = "{start_time:" + GetTimeStamp() + "}";
            SocketNetTools.SendMsg(timePackage);
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
