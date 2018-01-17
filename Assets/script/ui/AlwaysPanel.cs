/*************************************************************
   Copyright(C) 2017 by dayugame
   All rights reserved.
   
   AlwaysPanel.cs
   PartyRhythmGame
   
   Created by WuIslet on 2018-01-12.
   
*************************************************************/

using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace isletspace
{
    /// <summary>
    /// 
    /// </summary>
    public class AlwaysPanel : PanelBase
    {
        public RankBoard board;

        private void Start()
        {
            NetManager.Instance.AddEventListener(PacketType.RankListRet, OnRankRet);
        }

        public void OnRankRet(NetPacket msg)
        {
            string data = msg.data;
            board.SetAllRank(data);
        }
    }
}
