/*************************************************************
   Copyright(C) 2017 by dayugame
   All rights reserved.
   
   LogoMove.cs
   PartyRhythmGame
   
   Created by WuIslet on 2018-01-24.
   
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
    public class LogoMove : MonoBehaviour
    {
        private void Start()
        {
            transform.DORotate(Vector3.zero, 5);
            transform.DOMoveY(10, 10);
        }
    }
}
