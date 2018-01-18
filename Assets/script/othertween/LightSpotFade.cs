/*************************************************************
   Copyright(C) 2017 by dayugame
   All rights reserved.
   
   LightSpotFade.cs
   PartyRhythmGame
   
   Created by WuIslet on 2018-01-18.
   
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
    public class LightSpotFade : MonoBehaviour
    {
        private void Start()
        {
            var light = gameObject.GetComponent<Light>();
            DOTween.To(() => light.intensity, x => light.intensity = x, 0.5f, 3f).SetEase(Ease.OutQuad);
        }
    }
}
