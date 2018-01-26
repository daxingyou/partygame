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
            transform.DOKill();
            DOTween.To(() => 0.1f, x => transform.localScale = Vector3.one * x, 1, time).SetLoops(2, LoopType.Yoyo);


            var com = gameObject.GetComponent<Renderer>();
            com.material.DOKill();
            
            var start = com.material.color;
            start.a = 0;
            var mid = com.material.color;
            mid.a = 0.6f;
            com.material.DOColor(mid, time).SetLoops(2, LoopType.Yoyo);
            DOTween.To(() => start, x => com.material.color = x, mid, time).SetLoops(2, LoopType.Yoyo);
        }
    }
}
