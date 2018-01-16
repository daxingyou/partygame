/*************************************************************
   Copyright(C) 2017 by dayugame
   All rights reserved.
   
   SceneCamera.cs
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
    public class SceneCamera : MonoBehaviour
    {
        public void StartCamera()
        {
            gameObject.SetActive(true);
        }

        public void StopCamera()
        {
            gameObject.SetActive(false);
        }

        public void StartZoomCamera(int pos)
        {
            var camera = Utils.FindDirectChildComponent<MoveCamera>("MainCamera", transform);
            camera.DoCameraMove(pos);
        }
    }
}
