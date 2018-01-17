/*************************************************************
   Copyright(C) 2017 by dayugame
   All rights reserved.
   
   RankPanel.cs
   PartyRhythmGame
   
   Created by WuIslet on 2018-01-12.
   
*************************************************************/

using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

namespace isletspace
{
    /// <summary>
    /// 
    /// </summary>
    public class RankPanel : PanelBase
    {
        public RankBoard board;
        public Blur cam;

        private void Start()
        {
            NetManager.Instance.AddEventListener(PacketType.RankListRet, OnRankRet);
        }

        private void OnDisable()
        {
            NetManager.Instance.RemoveEventListener(PacketType.RankListRet, OnRankRet);
        }

        public override void DoStart(UIManager manager)
        {
            base.DoStart(manager);
            manager.EndPanel("AlwaysPanel");
            cam.enabled = true;
        }

        public override void DoEnd()
        {
            base.DoEnd();
            cam.enabled = false;
        }

        public void OnRankRet(NetPacket msg)
        {
            string data = msg.data;
            print("   receive  rank ret  " + data);
            board.SetAllRank(data);
        }
    }
}
