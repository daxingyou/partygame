/*************************************************************
   Copyright(C) 2017 by dayugame
   All rights reserved.
   
   JoinPanel.cs
   PartyRhythmGame
   
   Created by WuIslet on 2018-01-11.
   
*************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace isletspace
{
    /// <summary>
    /// 
    /// </summary>
    public class JoinPanel : PanelBase
    {
        public LeaderManager SceneManager;
        public Text playNum;
        public Time countDown;

        override public void DoStart(UIManager manager)
        {
            base.DoStart(manager);
            GameManager.UpdatePlayNumCallback += UpdatePlayNum;
            SceneManager.PlayJoin();
            countDown.StartCountDown(60);
        }

        override public void DoEnd()
        {
            base.DoEnd();
            GameManager.UpdatePlayNumCallback -= UpdatePlayNum;
            SceneManager.PlayEndJoin();
            countDown.StopCountDown();
        }

        public void UpdatePlayNum()
        {
            playNum.text = "当前参与人数：" + GameManager.currentPlayNum;
        }
    }
}
