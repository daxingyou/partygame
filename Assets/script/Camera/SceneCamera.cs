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
        public bool canChangeCamera;

        [Tooltip("For Debug")]
        public MoveCamera currentCamera;

        public void StartCamera()
        {
            gameObject.SetActive(true);
            if (canChangeCamera)
            {
                PickCamera();
                InvokeRepeating("JumpRandomCamera", 2.5f, 4);
            }
        }

        public void StopCamera()
        {
            gameObject.SetActive(false);
            if (currentCamera != null)
            {
                currentCamera.End();
                currentCamera = null;
                CancelInvoke("JumpRandomCamera");
            }
        }

        public void PickCamera(string name = null)
        {
            MoveCamera camera;
            if (string.IsNullOrEmpty(name))
            {
                int randidx = Random.Range(0, transform.childCount);
                camera = transform.GetChild(randidx).GetComponent<MoveCamera>();

                if (currentCamera != null && camera.name == currentCamera.name)
                {
                    camera = transform.GetChild((randidx + 1) % transform.childCount).GetComponent<MoveCamera>();
                }
            }
            else
            {
                camera = Utils.FindDirectChildComponent<MoveCamera>(name, transform);
            }

            camera.Begin();
            currentCamera = camera;
        }

        public void JumpCamera(string name = null)
        {
            if(currentCamera != null)
            {
                currentCamera.End();
            }
            PickCamera(name);
        }

        public void JumpRandomCamera()
        {
            JumpCamera();
        }

        /// <summary>
        /// 众舞者摄像机拉近
        /// </summary>
        /// <param name="target"></param>
        public void AllDancerCloseUp(Vector3 target)
        {
            CancelInvoke("JumpRandomCamera");
            if (currentCamera.name != "MainCamera")
            {
                JumpCamera("MainCamera");
            }
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
