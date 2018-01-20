/*************************************************************
   Copyright(C) 2017 by dayugame
   All rights reserved.
   
   ReadyPanel.cs
   PartyRhythmGame
   
   Created by WuIslet on 2018-01-18.
   
*************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace isletspace
{
    /// <summary>
    /// 
    /// </summary>
    public class ReadyPanel : PanelBase
    {
        public Time countDown;

        override public void DoStart(UIManager manager)
        {
            base.DoStart(manager);
            GameManager.InitGamePhase();
            GameManager.gamePhase = 1;//TODEL!   test
            countDown.StartCountDown(4);
        }
    }
}
