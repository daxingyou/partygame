/*************************************************************
   Copyright(C) 2017 by dayugame
   All rights reserved.
   
   OPPanel.cs
   PartyRhythmGame
   
   Created by WuIslet on 2018-01-11.
   
*************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace isletspace
{
    /// <summary>
    /// 
    /// </summary>
    public class OPPanel : PanelBase
    {
        public LeaderManager SceneManager;
        override public void DoStart(UIManager manager)
        {
            base.DoStart(manager);
            SceneManager.PlayOP();
        }
    }
}
