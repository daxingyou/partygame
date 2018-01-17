/*************************************************************
   Copyright(C) 2017 by dayugame
   All rights reserved.
   
   Director.cs
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
    public class Director : MonoBehaviour
    {
        public SceneCamera currentCamera;

        private void Start()
        {
            ChangeCamera("LeaderScene");
        }

        public void ChangeCamera(string name)
        {
            if (currentCamera != null && currentCamera.gameObject.name == name)
            {
                return;
            }

            var scene = Utils.FindDirectChildComponent<SceneCamera>(name, transform);
            if(currentCamera != null)
            {
                currentCamera.StopCamera();
            }
            scene.StartCamera();
            currentCamera = scene;
        }

        public void DoAllDancerCameraZoom(int pos)
        {
            if(currentCamera.gameObject.name != "AllDancerScene")
            {
                ChangeCamera("AllDancerScene");
            }
            currentCamera.StartZoomCamera(pos);
        }
    }
}
