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
            if (canChangeCamera)  // TODO ͳһ�����л���������Ƿ�������ж������������ĵط��������и�ȡ������ķ����� ע�⣬����������ע�᷽�����ص����֡�
            {
                InvokeRepeating("JumpRandomCamera", 0, 3);
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

        public void JumpCamera(string name)
        {
            if(currentCamera != null && currentCamera.name == name)
            {
                return;
            }

            if (currentCamera != null)
            {
                currentCamera.End();
            }

            var camera = Utils.FindDirectChildComponent<MoveCamera>(name, transform);
            camera.Begin();
            currentCamera = camera;
        }

        public void JumpRandomCamera()
        {
            MoveCamera camera;
            int randidx = Random.Range(0, transform.childCount);
            camera = transform.GetChild(randidx).GetComponent<MoveCamera>();

            if(currentCamera != null)
            {
                if(camera.name == currentCamera.name) //���������ͬһ�������
                {
                    camera = transform.GetChild((randidx + 1) % transform.childCount).GetComponent<MoveCamera>();
                }
                currentCamera.End();
            }
            
            camera.Begin();
            currentCamera = camera;
        }

        /// <summary>
        /// ���������������
        /// </summary>
        /// <param name="target"></param>
        public void AllDancerCloseUp(Vector3 target)
        {
            CancelInvoke("JumpRandomCamera");
            JumpCamera("MainCamera");
            var camera = Utils.FindDirectChildComponent<LookUpCamera>("MainCamera", transform);
            camera.DoCameraMove(target);
        }

        /// <summary>
        /// �����������ת
        /// </summary>
        public void CheerRotate()  //TODO ͳһ����camera�µ��ƶ�����
        {
            var camera = transform.Find("CheerCamera");
            camera.rotation = Quaternion.Euler(0, -15, 0);
            camera.DORotate(new Vector3(0, 15f, 0), 5f).SetEase(Ease.Linear);
        }

        /// <summary>
        /// ������������黯
        /// </summary>
        public void AllDancerBlur()
        {
            CancelInvoke("JumpRandomCamera");
            JumpCamera("MainCamera");
            var camera = Utils.FindDirectChildComponent<UnityStandardAssets.ImageEffects.Blur>("MainCamera", transform);
            camera.enabled = true;
        }

        /// <summary>
        /// ��������л������������
        /// </summary>
        /// <param name="name">���������</param>
        public void CancelRandomJumpCamera(string name)
        {
            CancelInvoke("JumpRandomCamera");
            JumpCamera(name);
        }
    }
}
