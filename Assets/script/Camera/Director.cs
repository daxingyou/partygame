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
    public class Director : ISingleton<Director>
    {
        [Tooltip("For Debug")]
        public SceneCamera currentCamera;

        public void ChangeCamera(string name)
        {
            if (currentCamera != null && currentCamera.gameObject.name == name)
            {
                return;
            }
            StartCoroutine(changeRoute(name));
        }

        IEnumerator changeRoute(string name)
        {
            FlashScreen.Instance.DoFlash(Color.black);

            yield return new WaitForSeconds(0.1f);

            var scene = Utils.FindDirectChildComponent<SceneCamera>(name, transform);
            if (currentCamera != null)
            {
                currentCamera.StopCamera();
            }
            scene.StartCamera();
            currentCamera = scene;
        }

        public void DoAllDancerCloseUp(Vector3 target)
        {
            if(currentCamera.gameObject.name != "AllDancerScene")
            {
                ChangeCamera("AllDancerScene");
            }
            currentCamera.AllDancerCloseUp(target);
        }

        public void DoCheerRotate()
        {
            if (currentCamera.gameObject.name != "CheerScene" && currentCamera.gameObject.name != "SecondCheerScene")
            {
                ChangeCamera("CheerScene");
            }
            currentCamera.CheerRotate();
        }
    }
}
