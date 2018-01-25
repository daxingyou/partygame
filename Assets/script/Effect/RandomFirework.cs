/*************************************************************
   Copyright(C) 2017 by dayugame
   All rights reserved.
   
   RandomFirework.cs
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
    public class RandomFirework : MonoBehaviour
    {
        private void OnEnable()
        {
            InvokeRepeating("CreateFirework", 0, 0.5f);
        }

        private void CreateFirework()
        {
            StartCoroutine(DelayCreateFirework());
        }

        private IEnumerator DelayCreateFirework()
        {
            int time = Random.Range(1, 3);
            for (int i = 0; i < time; ++i)
            {
                float gap = Random.Range(0, 0.8f);
                yield return new WaitForSeconds(gap);

                var firework = Pool.CreateObject("firework", transform, new Vector3(Random.Range(-10f, 10f), Random.Range(-4f, 7f), Random.Range(-3f, 3f)));
                Destroy(firework, 1.6f);
            }
        }

        private void OnDisable()
        {
            CancelInvoke("CreateFirework");
        }
    }
}
