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

        private Blur currentCameraBlur;

        private void Start()
        {
            GameManager.RankCallback += OnRankRet;
        }

        private void OnDestroy()
        {
            GameManager.RankCallback -= OnRankRet;
        }

        public override void DoStart(UIManager manager)
        {
            base.DoStart(manager);
            manager.EndPanel("AlwaysPanel");
            currentCameraBlur = Utils.GetChildComponent<Blur>(Director.Instance.currentCamera.transform);
            if (currentCameraBlur != null)
            {
                currentCameraBlur.enabled = true;
            }
        }

        public override void DoEnd()
        {
            base.DoEnd();
            if(currentCameraBlur != null)
            {
                currentCameraBlur.enabled = false;
                currentCameraBlur = null;
            }
        }

        public void OnRankRet()
        {
            board.SetAllRank();
        }
    }
}
