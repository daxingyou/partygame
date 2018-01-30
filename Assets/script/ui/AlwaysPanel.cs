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
using System.Linq;
using Newtonsoft.Json;
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
            List<RankVO> top5 = new List<RankVO>(5);
            top5.AddRange(GameManager.RankData);

            if (top5.Count > 5)
            {
                top5.RemoveRange(5, top5.Count - 5);
            }
            board.SetAllRank("UI/smallone", top5);
        }
    }
}
