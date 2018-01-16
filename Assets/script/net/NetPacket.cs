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
        HeartBeat           = 0,        //��
        AccountCountRet     = 100,      //���ӿͻ�������
        RankListRet         = 101,      //�������а�����
        UploadScore         = 102,      //�ͻ����ϴ�ÿ�ֵ÷�
        UploadStartTime     = 103,      //�ͻ����ϴ���ʼʱ��
        GetTimestamp     	= 104,      //��ȡʱ���
        ScoreRequest    	= 105,      //��ȡ�÷�����
        ConnectSucc    		= 106,      //���ӳɹ�
        MsgAck    			= 107,      //��Ϣȷ�Ϸ���
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
