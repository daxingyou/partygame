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
        public ProgressBar bar;

        int lightSpotCnt;

        public override void DoStart(UIManager manager)
        {
            base.DoStart(manager);
            SceneManager.AllDrumList();
            Invoke("PlayLightSpotAll", currentTime - 6);

            GameManager.phaseTime += 1;
        }

        public void PlayLightSpotAll()
        {
            lightSpotCnt = SceneManager.PlayLightSpotAll(LightSpotCallback);
        }

        public void LightSpotCallback()
        {
            float interval = 1f / 3 / ImportRoute.getPhase().Count / lightSpotCnt;
            bar.AddProgress(interval);
        }
    }
}
