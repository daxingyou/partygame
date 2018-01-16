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
        public int time = 0;

        public void DoCheer()
        {
            for (int i = 0; i < allDancer.childCount; ++i)
            {
                var ani = allDancer.GetChild(i).GetComponent<DancerAni>();
                ani.DoCheer(true);
            }

            if(time >= allTorch.childCount)
            {
                return;
            }

            var torch = allTorch.GetChild(time).GetComponent<ParticleAndAnimation>();
            torch.gameObject.SetActive(true);
            torch.PlayOnce();
        }

        public void CheerEnd()
        {
            for (int i = 0; i < allDancer.childCount; ++i)
            {
                var ani = allDancer.GetChild(i).GetComponent<DancerAni>();
                ani.DoCheer(false);
            }

            if (time >= allTorch.childCount)
            {
                return;
            }

            var torch = allTorch.GetChild(time).GetComponent<ParticleAndAnimation>();
            torch.gameObject.SetActive(false);
            torch.Stop();

            time += 1;
        }
    }
}
