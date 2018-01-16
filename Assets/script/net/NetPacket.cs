/*************************************************************
   Copyright(C) 2017 by dayugame
   All rights reserved.
   
   NetPacket.cs
   PartyRhythmGame
   
   Created by WuIslet on 2018-01-12.
   
*************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace isletspace
{
    public enum PacketType
    {
        HeartBeat           = 0,        //空
        AccountCountRet     = 100,      //连接客户端数量
        RankListRet         = 101,      //返回排行榜数据
        UploadScore         = 102,      //客户端上传每轮得分
        UploadStartTime     = 103,      //客户端上传开始时间
        GetTimestamp     	= 104,      //获取时间戳
        ScoreRequest    	= 105,      //获取得分数据
        ConnectSucc    		= 106,      //连接成功
        MsgAck    			= 107,      //消息确认返回
    }

    /// <summary>
    /// 
    /// </summary>
    public class NetPacket
    {
        public PacketType msg_id;
        public string data;
    }
}
