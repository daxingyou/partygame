/*************************************************************
   Copyright(C) 2017 by dayugame
   All rights reserved.
   
   PlayTurnPanel.cs
   PartyRhythmGame
   
   Created by WuIslet on 2018-01-17.
   
*************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace isletspace
{
    /// <summary>
    /// 
    /// </summary>
    public class PlayTurnPanel : PanelBase
    {
        public AllDancerManager SceneManager;

        public override void DoStart(UIManager manager)
        {
            base.DoStart(manager);
            SceneManager.AllRandomDrum(nextPanelOrder[nextPanelOrder.Count - 1].timeout);
            Invoke("PlayLightSpotAll", 7);
        }

        public void PlayLightSpotAll()
        {
            SceneManager.PlayLightSpotAll();
        }
    }
}
