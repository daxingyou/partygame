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
        List<RankVO> lastData;

        public void SetAllRank(string json)
        {
            JsonSerializer serializer = new JsonSerializer();
            StringReader sr = new StringReader(json);
            List<RankVO> data = serializer.Deserialize(new JsonTextReader(sr), typeof(List<RankVO>)) as List<RankVO>;

            //if(lastData == null)
            {
                if(transform.childCount > 0)
                {
                    for (int i = transform.childCount - 1; i >= 0; --i)
                    {
                        Destroy(transform.GetChild(i).gameObject);
                    }
                }

                lastData = data;
                for (int i = 0; i < data.Count; ++i)
                {
                    var obj = Pool.CreateObject("UI/one", transform);
                    var one = obj.GetComponent<OneRank>();
                    one.SetAllData(data[i]);
                }
            }
            //else
            //{
                //diff lastData & data;
                //play diff ani
            //}
        }
    }
}