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

        public Vector3 CloseUp(int pos)
        {
            var target = allDancer.GetChild(pos);

            var ani = target.GetComponent<DancerAni>();
            ani.DoPose();

            return target.position;
        }

        public void PlayLightSpot(int pos)
        {
            var ani = allDancer.GetChild(pos).GetComponent<DancerAni>();
            ani.DoLightSpotMove();
        }

        public int PlayLightSpotAll(System.Action callback)
        {
            int total = Random.Range((int)(allDancer.childCount / 2), (int)(allDancer.childCount * 0.7f));
            for (int num = 0; num < total; ++num)
            {
                int idx = Random.Range(0, allDancer.childCount);
                float starttime = Random.Range(0f, 1.8f);
                var ani = allDancer.GetChild(idx).GetComponent<DancerAni>();
                ani.DoLightSpotMove(starttime, callback);
            }
            return total;
        }

        public void AllDrumRandom(float time)
        {
            for (int i = 0; i < allDancer.childCount; ++i)
            {
                var ani = allDancer.GetChild(i).GetComponent<DancerAni>();
                if (ani.gameObject.activeSelf)
                {
                    StartCoroutine(ani.DrumRandom(10, time));
                }
            }
        }

        public void AllDrumConstant()
        {
            for (int i = 0; i < allDancer.childCount; ++i)
            {
                var ani = allDancer.GetChild(i).GetComponent<DancerAni>();
                if (ani.gameObject.activeSelf)
                {
                    StartCoroutine(ani.DrumConstant(1, 10, 0.8f));
                }
            }
        }

        public void AllDrumList()
        {
            for (int i = 0; i < allDancer.childCount; ++i)
            {
                var ani = allDancer.GetChild(i).GetComponent<DancerAni>();
                if (ani.gameObject.activeSelf)
                {
                    StartCoroutine(ani.DrumList(ImportRoute.GetBeat(), ImportRoute.GetBeatTime()));
                }
            }
        }
    }
}
