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
            GameManager.RankCallback += OnRankRet;
        }

        private void OnDestroy()
        {
            GameManager.RankCallback -= OnRankRet;
        }

        public void OnRankRet()
        {
            board.SetAllRank();
        }
    }
}
