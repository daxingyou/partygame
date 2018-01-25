/*************************************************************
   Copyright(C) 2017 by dayugame
   All rights reserved.
   
   EDPanel.cs
   PartyRhythmGame
   
   Created by WuIslet on 2018-01-24.
   
*************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace isletspace
{
    /// <summary>
    /// 
    /// </summary>
    public class EDPanel : PanelBase
    {
        public AllDancerManager SceneManager;
        override public void DoStart(UIManager manager)
        {
            base.DoStart(manager);
            FlashScreen.Instance.DoUnCover(0.1f);
            Director.Instance.RegistChangeCamera(cameraScene, DoEndCamera);
        }

        private void DoEndCamera(SceneCamera camera)
        {
            camera.CancelRandomJumpCamera("MainCamera");
            Director.Instance.UnRegistChangeCamera(DoEndCamera);
        }
    }
}
