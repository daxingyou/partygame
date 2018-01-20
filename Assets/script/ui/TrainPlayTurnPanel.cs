/*************************************************************
   Copyright(C) 2017 by dayugame
   All rights reserved.
   
   TrainPlayTurnPanel.cs
   PartyRhythmGame
   
   Created by WuIslet on 2018-01-20.
   
*************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace isletspace
{
    /// <summary>
    /// 
    /// </summary>
    public class TrainPlayTurnPanel : PanelBase
    {
        public override void DoStart(UIManager manager)
        {
            base.DoStart(manager);
            GameManager.phaseTime += 1;
        }
    }
}
