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

        public void DoFlash(Color color)
        {
            color.a = 0;
            image.color = color;
            image.DOFade(1, 0.3f).SetLoops(2, LoopType.Yoyo);
        }

        public void DoCover(Color color)
        {
            color.a = 0;
            image.color = color;
            image.DOFade(1, 0.3f);
        }

        public void DoUnCover()
        {
            image.DOFade(0, 0.3f);
        }
    }
}
