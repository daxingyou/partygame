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
        /*
        string data0 =
                "[{\"name\":\"爱我中华\"}]";

        string data1 =
                "[{\"id\":1, \"score\":0000, \"name\":\"爱我中华\", \"pic_url\":\"www.g.cn\"}," +
                "]";

        string data2 =
                "[{id:1, score:9000, name:\"-test0000\", headimg:\"www.g.cn\"}," +
                "{id:2, score:8000, name:\"-test1000\", headimg:\"www.g.cn\"}," +
                "{id:3, score:7000, name:\"-test2000\", headimg:\"www.g.cn\"}," +
                "{id:4, score:6000, name:\"-test3000\", headimg:\"www.g.cn\"}," + 
                "{id:5, score:5000, name:\"-test4000\", headimg:\"www.g.cn\"}," +
                "{id:6, score:4000, name:\"-test5000\", headimg:\"www.g.cn\"}," +
                "{id:7, score:3000, name:\"-test6000\", headimg:\"www.g.cn\"}," +
                "{id:8, score:2000, name:\"-test7000\", headimg:\"www.g.cn\"}," +
                "{id:9, score:1000, name:\"-test8000\", headimg:\"www.g.cn\"}," +
                "{id:0, score:0000, name:\"-test9000\", headimg:\"www.g.cn\"}," +
                "]";

        public void test()
        {
            var t1 = Encoding.UTF8.GetBytes(data0);
            print(t1);
            var t2 = Encoding.UTF8.GetString(t1);
            print(t2);
            SetAllRank(data1);
            StartCoroutine(test2());
        }

        IEnumerator test2()
        {
            yield return new WaitForSeconds(3);
            SetAllRank(data2);
        } */

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
                    for (int i = transform.childCount - 1; i >= 1; --i)
                    {
                        Destroy(transform.GetChild(i).gameObject);
                    }
                }

                lastData = data;
                for (int i = 0; i < data.Count; ++i)
                {
                    var prefab = Resources.Load("prefab/UI/one") as GameObject;
                    var obj = Instantiate(prefab, Vector3.one, Quaternion.identity, transform) as GameObject;
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