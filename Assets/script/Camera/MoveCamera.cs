/*************************************************************
   Copyright(C) 2017 by dayugame
   All rights reserved.
   
   MoveCamera.cs
   PartyRhythmGame
   
   Created by WuIslet on 2018-01-23.
   
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
    public class MoveCamera : MonoBehaviour
    {
        protected Vector3 StartPos;
        protected Vector3 StartRotate;
        protected DOTweenPath pathMgr;

        void Awake()
        {
            pathMgr = gameObject.GetComponent<DOTweenPath>();
            RecordStartTransorm();
        }

        private void RecordStartTransorm()
        {
            StartPos = transform.position;
            StartRotate = transform.rotation.eulerAngles;
        }

        public void Begin()
        {
            RecordStartTransorm();
            gameObject.SetActive(true);
            StartMove();
        }

        public void End()
        {
            ResetPos();
            gameObject.SetActive(false);
        }

        public void ResetPos()
        {
            StopMove();
            transform.position = StartPos;
            transform.rotation = Quaternion.Euler(StartRotate);
        }

        virtual public void StartMove()
        {
            if(pathMgr != null)
            {
                pathMgr.DOPlay();
            }
        }

        virtual public void StopMove()
        {
            if (pathMgr != null)
            {
                pathMgr.DOPause();
            }
        }
    }
}
