/*************************************************************
   Copyright(C) 2017 by dayugame
   All rights reserved.
   
   CheerManager.cs
   PartyRhythmGame
   
   Created by WuIslet on 2018-01-16.
   
*************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace isletspace
{
    /// <summary>
    /// 
    /// </summary>
    public class CheerManager : MonoBehaviour
    {
        public Transform allDancer;
        public Transform allTorch;
        public Transform allEffect;
        public GameObject allLight;

        public void DoCheer()
        {
            for (int i = 0; i < allDancer.childCount; ++i)
            {
                var ani = allDancer.GetChild(i).GetComponent<DancerAni>();
                ani.DoCheer(true);
            }

            allLight.SetActive(true);

            if (0 <= GameManager.gamePhase && GameManager.gamePhase < 2)
            {
                StartCoroutine(PlayTorchChange());
            }
        }

        IEnumerator PlayTorchChange()
        {
            yield return new WaitForSeconds(1);
            
            var effect = allEffect.GetChild(0).GetComponent<ParticleAndAnimation>();
            effect.gameObject.SetActive(true);
            effect.PlayOnce();

            yield return new WaitForSeconds(1);
            
            var torch = allTorch.GetChild(0).gameObject;
            torch.SetActive(false);
            GameManager.AddGamePhase();
        }

        public void CheerEnd()
        {
            allLight.SetActive(true);
            for (int i = 0; i < allDancer.childCount; ++i)
            {
                var ani = allDancer.GetChild(i).GetComponent<DancerAni>();
                ani.DoCheer(false);
            }
        }
    }
}
