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
        [Tooltip("For Debug")]
        public MoveCamera currentCamera;

        private void Awake()
        {
            //StartCamera();
        }

        public void StartCamera()
        {
            gameObject.SetActive(true);
            PickCamera();
            InvokeRepeating("JumpCamera", 3, 4);
        }

        public void StopCamera()
        {
            gameObject.SetActive(false);
        }

        public void PickCamera()
        {
            int randidx = Random.Range(0, transform.childCount);
            //randidx = 0;
            var camera = transform.GetChild(randidx).GetComponent<MoveCamera>();

            if(currentCamera != null && camera.name == currentCamera.name)
            {
                camera = transform.GetChild((randidx + 1) % transform.childCount).GetComponent<MoveCamera>();
            }

            camera.Begin();
            currentCamera = camera;
        }

        public void JumpCamera()
        {
            if(currentCamera != null)
            {
                currentCamera.End();
            }
            PickCamera();
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
            var camera = Utils.FindDirectChildComponent<LookUpCamera>("MainCamera", transform);
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
