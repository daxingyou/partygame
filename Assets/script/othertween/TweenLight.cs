/*************************************************************
   Copyright(C) 2017 by dayugame
   All rights reserved.
   
   opLight.cs
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
    public class TweenLight : MonoBehaviour
    {
        public void Begin()
        {
            gameObject.SetActive(true);
            for (int i = 0; i < transform.childCount; ++i)
            {
                var child = transform.GetChild(i).GetComponent<Light>();
                DOTween.To(() => child.intensity, x => child.intensity = x, 0, 0.25f).From();
            }
        }

        public void End()
        {
            for (int i = 0; i < transform.childCount; ++i)
            {
                var child = transform.GetChild(i).GetComponent<Light>();
                DOTween.To(() => child.intensity, x => child.intensity = x, 0, 0.25f);
            }
            Invoke("DelayActiveFalse", 0.25f);
        }

        private void DelayActiveFalse()
        {
            gameObject.SetActive(false);
        }
    }
}
