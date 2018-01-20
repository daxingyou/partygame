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
using DG.Tweening;

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

        //TODO  random camera func
        //TODO  jump camera func

        /// <summary>
        /// 众舞者摄像机拉近
        /// </summary>
        /// <param name="target"></param>
        public void AllDancerCloseUp(Vector3 target)
        {
            //TODO  if not main camera, jump to main.
            var camera = Utils.FindDirectChildComponent<MoveCamera>("MainCamera", transform);
            camera.DoCameraMove(target);
        }

        /// <summary>
        /// 欢呼摄像机旋转
        /// </summary>
        public void CheerRotate()
        {
            var camera = transform.Find("CheerCamera");
            camera.rotation = Quaternion.Euler(0, -15, 0);
            camera.DORotate(new Vector3(0, 15f, 0), 4f).SetEase(Ease.Linear);
        }
    }
}
