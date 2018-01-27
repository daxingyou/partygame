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
        public void SetAllRank(string prefabName, List<RankVO> data)
        {
            if (transform.childCount > 0)
            {
                for (int i = transform.childCount - 1; i >= 0; --i)
                {
                    Destroy(transform.GetChild(i).gameObject);
                }
            }

            print("   rank   data   " + JsonConvert.SerializeObject(data));
            
            for (int i = 0; i < data.Count; ++i)
            {
                var obj = Pool.CreateObject(prefabName, transform);
                var one = obj.GetComponent<OneRank>();
                one.SetAllData(data[i]);
            }
        }
    }
}