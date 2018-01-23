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
            if (string.IsNullOrEmpty(isChanging))     // 没有在改变摄像机
            {
                if(changeName == currentCamera.name)  // 现在就是要的摄像机
                {
                    callback(currentCamera);          // 直接执行
                }
                else if (ChangeCamera(changeName))    // 如果成功执行切换摄像机
                {
                    afterChangeCamera += callback;    // 进行注册
                }
            }
            else                                      // 正在改变摄像机
            {
                if (changeName == isChanging)         // 是改变成想要的摄像机
                {
                    afterChangeCamera += callback;    // 追加注册
                }
                else                                  // 不是改变成想要的摄像机
                {
                    return;                           // 拒绝这次注册
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
