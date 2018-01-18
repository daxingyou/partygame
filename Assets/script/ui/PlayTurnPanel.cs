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
            SceneManager.AllRandomDrum(timeout);
            Invoke("DoCloseUp", 3);
            InvokeRepeating("test", 1, 7);
        }

        public void DoCloseUp()
        {
            int pos = 3;
            Vector3 target = SceneManager.CloseUp(pos);
            Director.Instance.DoAllDancerCloseUp(target);
        }

        public void test()
        {
            SceneManager.PlayLightSpotAll();
        }
    }
}
