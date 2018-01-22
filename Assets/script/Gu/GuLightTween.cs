/*************************************************************
   Copyright(C) 2017 by dayugame
   All rights reserved.
   
   GuLightTween.cs
   PartyRhythmGame
   
   Created by WuIslet on 2018-01-22.
   
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
    public class GuLightTween : MonoBehaviour
    {
        public void LightOnce(float time)
        {
            var com = gameObject.GetComponent<Renderer>();
            var mid = com.material.color;
            mid.a = 1f;
            DOTween.To(() => com.material.color, x => com.material.color = x, mid, time).SetLoops(2, LoopType.Yoyo);
            transform.DOScale(1, time).SetLoops(2, LoopType.Yoyo);
        }
    }
}
