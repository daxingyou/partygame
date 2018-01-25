/*************************************************************
   Copyright(C) 2017 by dayugame
   All rights reserved.
   
   EDManager.cs
   PartyRhythmGame
   
   Created by WuIslet on 2018-01-25.
   
*************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace isletspace
{
    /// <summary>
    /// 
    /// </summary>
    public class EDManager : MonoBehaviour
    {
        public Transform allDancer;
        public Transform allEffect;
        public GameObject allLight;

        public void DoCheer()
        {
            for (int i = 0; i < allDancer.childCount; ++i)
            {
                var ani = allDancer.GetChild(i).GetComponent<DancerAni>();
                ani.DoCheer(true);
            }

            for (int i = 0; i < allEffect.childCount; ++i)
            {
                var obj = allEffect.GetChild(i);
                obj.gameObject.SetActive(true);
                var effect = obj.GetComponent<ParticleAndAnimation>();
                if (effect != null)
                {
                    effect.PlayOnce();
                }
            }

            //allLight.SetActive(true);
        }

        public void CheerEnd()
        {
            //allLight.SetActive(true);
            for (int i = 0; i < allDancer.childCount; ++i)
            {
                var ani = allDancer.GetChild(i).GetComponent<DancerAni>();
                ani.DoCheer(false);
            }

            for (int i = 0; i < allEffect.childCount; ++i)
            {
                var obj = allEffect.GetChild(i);
                var effect = obj.GetComponent<ParticleAndAnimation>();
                if (effect != null)
                {
                    effect.Stop();
                }
                obj.gameObject.SetActive(false);
            }
        }
    }
}
