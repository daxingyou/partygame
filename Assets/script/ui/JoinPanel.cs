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
        public Text playNum;

        override public void DoStart(UIManager manager)
        {
            base.DoStart(manager);
            transform.DOLocalMoveX(0, 1);
            GameManager.UpdatePlayNumCallback += UpdatePlayNum;
        }

        override public void DoEnd()
        {
            base.DoEnd();
            transform.DOLocalMoveX(900, 1);
            GameManager.UpdatePlayNumCallback -= UpdatePlayNum;
        }

        public void UpdatePlayNum()
        {
            playNum.text = "参与人数：" + GameManager.currentPlayNum + "/100";
        }
    }
}
