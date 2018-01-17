/*************************************************************
   Copyright(C) 2017 by dayugame
   All rights reserved.
   
   LeaderManager.cs
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
    public class LeaderManager : MonoBehaviour
    {
        public GameObject spotLight;
        public DancerAni dancer;

        public void PlayOP()
        {
            spotLight.SetActive(false);
            dancer.PlayOP();
            Invoke("PlayEndOP", 4);
        }

        public void PlayEndOP()
        {
            spotLight.SetActive(true);
            dancer.PlayEndOP();
            //TODO ºÚÆÁ×à¹ÄÉù
            //ÑÓ³Ùµ÷ÓÃ ÁÁÆÁ¡£
        }

        public void PlayJoin()
        {
            dancer.PlayJoin();
        }

        public void PlayEndJoin()
        {
            dancer.PlayEndJoin();
        }
    }
}
