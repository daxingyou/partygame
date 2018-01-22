/*************************************************************
   Copyright(C) 2017 by dayugame
   All rights reserved.
   
   FireLightTween.cs
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
    public class FireLightTween : MonoBehaviour
    {
        private void Start()
        {
            var light = gameObject.GetComponent<Light>();
            DOTween.To(() => light.intensity, x => light.intensity = x, 7f, 1f).SetLoops(-1, LoopType.Yoyo);
        }
    }
}
