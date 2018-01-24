/*************************************************************
   Copyright(C) 2017 by dayugame
   All rights reserved.
   
   InterPanel.cs
   PartyRhythmGame
   
   Created by WuIslet on 2018-01-22.
   
*************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace isletspace
{
    /// <summary>
    /// 
    /// </summary>
    public class InterPanel : PanelBase
    {
        public Time countDown;

        override public void DoStart(UIManager manager)
        {
            base.DoStart(manager);
            countDown.StartCountDown((int)currentTimeOut);
        }

        override public void DoEnd()
        {
            base.DoEnd();
            countDown.StopCountDown();
            SoundManager.Instance.StopBackground();
        }
    }
}
