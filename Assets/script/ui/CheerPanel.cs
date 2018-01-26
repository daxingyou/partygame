/*************************************************************
   Copyright(C) 2017 by dayugame
   All rights reserved.
   
   CheerPanel.cs
   PartyRhythmGame
   
   Created by WuIslet on 2018-01-16.
   
*************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace isletspace
{
    /// <summary>
    /// 
    /// </summary>
    public class CheerPanel : PanelBase
    {
        public CheerManager SceneManager;

        override public void DoStart(UIManager manager)
        {
            base.DoStart(manager);
            FlashScreen.Instance.DoUnCover(1f);
            SoundManager.Instance.PlayCheerFX();
            SoundManager.Instance.FadeBackground(currentTimeOut);
            DoCheer();
        }

        override public void DoEnd()
        {
            base.DoEnd();
            SceneManager.CheerEnd();
        }

        private void DoCheer()
        {
            SceneManager.DoCheer();
            Director.Instance.RegistChangeCamera(cameraScene, DoCheerRotate);
        }

        private void DoCheerRotate(SceneCamera camera)
        {
            camera.CheerRotate();
            Director.Instance.UnRegistChangeCamera(DoCheerRotate);
        }
    }
}
