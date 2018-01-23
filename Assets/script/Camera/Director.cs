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

        [Tooltip("For Debug")]
        public string isChanging;

        public delegate void callType(SceneCamera camera);
        public callType afterChangeCamera;

        public bool ChangeCamera(string name)
        {
            if (!string.IsNullOrEmpty(isChanging))
            {
                return false;
            }
            
            if (currentCamera != null && currentCamera.name == name)
            {
                return false;
            }
            isChanging = name;

            StartCoroutine(changeRoute(name));
            return true;
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

            if(afterChangeCamera != null)
            {
                afterChangeCamera(currentCamera);
                System.Delegate.RemoveAll(afterChangeCamera, afterChangeCamera);
            }

            isChanging = "";
        }

        public void RegistChangeCamera(string changeName, callType callback)
        {
            if (string.IsNullOrEmpty(isChanging))     // û���ڸı������
            {
                if(changeName == currentCamera.name)  // ���ھ���Ҫ�������
                {
                    callback(currentCamera);          // ֱ��ִ��
                }
                else if (ChangeCamera(changeName))    // ����ɹ�ִ���л������
                {
                    afterChangeCamera += callback;    // ����ע��
                }
            }
            else                                      // ���ڸı������
            {
                if (changeName == isChanging)         // �Ǹı����Ҫ�������
                {
                    afterChangeCamera += callback;    // ׷��ע��
                }
                else                                  // ���Ǹı����Ҫ�������
                {
                    return;                           // �ܾ����ע��
                }
            }
        }

        public void UnRegistChangeCamera(callType callback)
        {
            if(afterChangeCamera != null)
            {
                afterChangeCamera -= callback;
            }
        }

        //public void DoCheerRotate()
        //{
        //    if (currentCamera.gameObject.name != "CheerScene" && !isChanging)
        //    {
        //        ChangeCamera("CheerScene");
        //    }

        //    afterChangeCamera += currentCamera.CheerRotate;
        //}

        //public void DoSecondCheerRotate()
        //{
        //    if (currentCamera.gameObject.name != "SecondCheerScene" && !isChanging)
        //    {
        //        ChangeCamera("SecondCheerScene");
        //    }
        //    currentCamera.CheerRotate();
        //}

        //public void DoAllDancerCloseUp(Vector3 target)
        //{
        //    if (currentCamera.gameObject.name != "AllDancerScene")
        //    {
        //        ChangeCamera("AllDancerScene");
        //    }
        //    currentCamera.AllDancerCloseUp(target);
        //}
    }
}
