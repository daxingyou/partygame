/*************************************************************
   Copyright(C) 2017 by dayugame
   All rights reserved.
   
   MoveCamera.cs
   PartyRhythmGame
   
   Created by WuIslet on 2018-01-08.
   
*************************************************************/

using UnityEngine;
using System.Collections;
using DG.Tweening;

namespace isletspace
{
    public class MoveCamera : MonoBehaviour
    {
        [SerializeField]
        private Transform Ground;

        public bool isPlaying;

        public Vector3 StartPos;
        public Vector3 StartRotate;

        #region const
        private Vector3 LookFront = new Vector3(0, 180 - 40, 0);
        private Vector3 LookRotatePoint1 = new Vector3(0, 180, 0);
        private Vector3 LookRotatePoint2 = new Vector3(0, 180 + 40, 0);
        #endregion

        public int targetpos;

        void Awake()
        {
            StartPos = transform.position;
            StartRotate = transform.rotation.eulerAngles;
            DoCameraMove(targetpos);
        }

        public int DoCameraMove(int num)
        {
            if (num < 0 || num >= Ground.childCount)
            {
                return -1;
            }

            if (isPlaying)
            {
                return -2;
            }

            Transform target = Ground.GetChild(num);
            var ani = target.GetComponent<DancerAni>();
            int a = Random.Range(1, 4);
            print("   do pose  " + a);
            ani.DoPose(a);

            isPlaying = true;

            Sequence seq = DOTween.Sequence();
            //ZoomIn
            seq.Append(transform.DOMove(target.position, 0.8f).SetEase(Ease.OutExpo));
            seq.Join(transform.DORotate(LookFront, 0.8f));
            //gap
            seq.AppendInterval(0.3f);
            //Rotate
            seq.Append(transform.DORotate(LookRotatePoint1, 1).SetEase(Ease.Linear));
            seq.Append(transform.DORotate(LookRotatePoint2, 1).SetEase(Ease.Linear));
            //gap
            seq.AppendInterval(0.3f);
            //ZoomOut
            seq.Append(transform.DOMove(StartPos, 0.8f));
            seq.Join(transform.DORotate(StartRotate, 0.8f));
            //PlayEnd
            seq.AppendCallback(() => { isPlaying = false; });

            return 0;
        }
    }
}