/*************************************************************
   Copyright(C) 2017 by dayugame
   All rights reserved.
   
   RankBoard.cs
   PartyRhythmGame
   
   Created by WuIslet on 2018-01-08.
   
*************************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace isletspace
{
    public class RankBoard : MonoBehaviour
    {
        public void SetAllRank()
        {
            var data = GameManager.RankData;
            for (int i = 0; i < data.Count; ++i)
            {
                var obj = Pool.CreateObject("UI/one", transform);
                var one = obj.GetComponent<OneRank>();
                one.SetAllData(data[i]);
            }
        }
    }
}