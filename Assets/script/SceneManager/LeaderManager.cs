/*************************************************************
   Copyright(C) 2017 by dayugame
   All rights reserved.
   
   LeaderManager.cs
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
    public class LeaderManager : MonoBehaviour
    {
        public TweenLight spotLight;
        public TweenLight fireLight;
        public GameObject torch;
        public DancerAni dancer;
        public Transform cameraLookPoint;
        
        [Tooltip("For Debug")]
        public bool isCameraFollow = false;

        private Transform mainCamera;
        private Vector3 oldCamera;

        public void PlayOP()
        {
            StartCoroutine(OPRoute());
        }

        IEnumerator OPRoute()
        {
            //初始化场景
            StartCameraFollow();
            yield return new WaitForSeconds(1f);
            dancer.PlayOP();
            yield return new WaitForSeconds(0.3f);
            //入场开灯
            spotLight.Begin();
            SoundManager.Instance.PlayLaZha();
            yield return new WaitForSeconds(3f);
            SoundManager.Instance.PlayFoot();
            yield return new WaitForSeconds(5.3f);
            spotLight.End();
            yield return new WaitForSeconds(0.5f);
            //黑屏奏鼓声
            SoundManager.Instance.PlaySoloBackground();
            EndCameraFollow();
            StartCoroutine(dancer.DrumSolo());
            yield return new WaitForSeconds(12f);
            //结束演奏
            dancer.PlayOPPose();
            torch.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            fireLight.Begin();
        }

        private void StartCameraFollow()
        {
            enabled = true;
            mainCamera = Director.Instance.currentCamera.transform;
            oldCamera = mainCamera.position;
            isCameraFollow = true;
        }

        private void EndCameraFollow()
        {
            if (mainCamera == null)
            {
                return;
            }

            mainCamera.position = oldCamera;
            mainCamera = null;
            isCameraFollow = false;
            enabled = false;
        }

        private void Update()
        {
            if(isCameraFollow && mainCamera != null)
            {
                Vector3 pos = cameraLookPoint.position;
                pos.y = 0;
                mainCamera.position = pos;
                spotLight.transform.position = pos;
            }
        }

        public void PlayJoin()
        {
            Invoke("DelayPlayJoin", 1);
        }

        private void DelayPlayJoin()
        {
            dancer.PlayJoin();
        }

        public void PlayEndJoin()
        {
            dancer.PlayEndJoin();
        }
    }
}
