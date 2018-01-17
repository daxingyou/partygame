/*************************************************************
   Copyright(C) 2017 by dayugame
   All rights reserved.
   
   AllDancerManager.cs
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
    public class AllDancerManager : MonoBehaviour
    {
        public Director director;
        public Transform allDancer;
        public Transform allTorch;

        private void Start()
        {
            GameManager.BeforeUpdateGamePhase += ChangePhase;
        }

        private void OnDestroy()
        {
            GameManager.BeforeUpdateGamePhase -= ChangePhase;
        }

        public void ChangePhase()
        {
            var torch = allTorch.GetChild(GameManager.gamePhase).gameObject;
            torch.SetActive(false);
            torch = allTorch.GetChild(GameManager.gamePhase + 1).gameObject;
            torch.SetActive(true);
        }

        public void CloseUp(int pos)
        {
            //1. camera to main
            //2. camera.zoomin
            //3. delay play pos
        }

    }
}
