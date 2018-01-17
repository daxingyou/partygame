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

        public void DoCheer()
        {
            for (int i = 0; i < allDancer.childCount; ++i)
            {
                var ani = allDancer.GetChild(i).GetComponent<DancerAni>();
                ani.DoCheer(true);
            }

            if (0 <= GameManager.gamePhase && GameManager.gamePhase < 2)
            {
                StartCoroutine(PlayTorchChange());
            }
        }

        IEnumerator PlayTorchChange()
        {
            yield return new WaitForSeconds(1);
            
            var effect = allEffect.GetChild(GameManager.gamePhase).GetComponent<ParticleAndAnimation>();
            effect.gameObject.SetActive(true);
            effect.PlayOnce();

            yield return new WaitForSeconds(3);
            
            var torch = allTorch.GetChild(GameManager.gamePhase).gameObject;
            torch.SetActive(false);
            torch = allTorch.GetChild(GameManager.gamePhase + 1).gameObject;
            torch.SetActive(true);

            effect.Stop();
            effect.gameObject.SetActive(false);

            GameManager.AddGamePhase();
        }

        public void CheerEnd()
        {
            for (int i = 0; i < allDancer.childCount; ++i)
            {
                var ani = allDancer.GetChild(i).GetComponent<DancerAni>();
                ani.DoCheer(false);
            }
        }
    }
}
