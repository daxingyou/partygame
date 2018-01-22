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
            //Invoke("DoCheer", 0.13f);
            //DoCheer();
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
            //if(gameObject.name == "CheerPanel")
            //{
            //    Director.Instance.DoCheerRotate();
            //}
            //else if(gameObject.name == "SecondCheerPanel")
            //{
            //    Director.Instance.DoSecondCheerRotate();
            //}
        }

        private void DoCheerRotate(SceneCamera camera)
        {
            camera.CheerRotate();
            Director.Instance.UnRegistChangeCamera(DoCheerRotate);
        }
    }
}
