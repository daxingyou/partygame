/*************************************************************
   Copyright(C) 2017 by dayugame
   All rights reserved.
   
   FlashScreen.cs
   PartyRhythmGame
   
   Created by WuIslet on 2018-01-17.
   
*************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace isletspace
{
    /// <summary>
    /// 
    /// </summary>
    public class FlashScreen : ISingleton<FlashScreen>
    {
        public RawImage image;

        [Tooltip("For Debug")]
        public bool isCover;

        public void DoFlash(Color color)
        {
            if(isCover)
            {
                return;
            }
            color.a = 0;
            image.color = color;
            image.DOFade(1, 0.3f).SetLoops(2, LoopType.Yoyo);
        }

        public void DoCover(Color color, float time)
        {
            isCover = true;
            color.a = 0;
            image.color = color;
            image.DOFade(1, time).SetEase(Ease.Linear);
        }

        public void DoUnCover(float time)
        {
            if(!isCover)
            {
                return;
            }
            image.DOFade(0, time).SetEase(Ease.Linear).onComplete = () => { isCover = false; };
        }
    }
}
