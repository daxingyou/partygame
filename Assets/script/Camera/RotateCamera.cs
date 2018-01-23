/*************************************************************
   Copyright(C) 2017 by dayugame
   All rights reserved.
   
   RotateCamera.cs
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
    public class RotateCamera : MoveCamera
    {
        public Vector3 endRotate;

        public override void StartMove()
        {
            base.StartMove();
            transform.DORotate(endRotate, 15);
        }

        public override void StopMove()
        {
            base.StopMove();
            transform.DOKill();
        }
    }
}
