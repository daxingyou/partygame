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

namespace isletspace
{
    /// <summary>
    /// 
    /// </summary>
    public class RankPanel : PanelBase
    {
        #region ³£Êý
        private Vector3 OriginPos = new Vector3(900, 600);
        #endregion
        public RankBoard board;

        public void OnRankRet(NetPacket msg)
        {
            string data = msg.data;
            print("   receive  rank ret  " + data );
            board.SetAllRank(data);
        }

        override public void DoStart(UIManager manager)
        {
            base.DoStart(manager);
            transform.DOLocalMove(Vector3.zero, 1);
            //board.test();
        }

        override public void DoEnd()
        {
            base.DoEnd();
            transform.DOLocalMove(OriginPos, 1);
        }
    }
}
