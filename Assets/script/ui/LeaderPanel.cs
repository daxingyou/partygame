/*************************************************************
   Copyright(C) 2017 by dayugame
   All rights reserved.
   
   LeaderPanel.cs
   PartyRhythmGame
   
   Created by WuIslet on 2018-01-11.
   
*************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

namespace isletspace
{
    public class beatData
    {
        public int type;
        public int time;
    }

    public class ArrowData
    {
        public int basetime;
        public List<beatData> beats;
    }

    /// <summary>
    /// 
    /// </summary>
    public class LeaderPanel : PanelBase
    {
        string data = "{basetime:19000,beats:[{type:1,time:20000}, {type:2,time:22000}, {type:3,time:22500}, {type:1,time:24500}, {type:2,time:25000}, ]}";
        
        public LeaderManager SceneManager;
        public ArrowManager arrowManager;

        public override void DoStart(UIManager manager)
        {
            base.DoStart(manager);
            SceneManager.dancer.DoYourTurn(false);
            StartPlay(data);
        }

        public void StartPlay(string json)
        {
            JsonSerializer serializer = new JsonSerializer();
            StringReader sr = new StringReader(json);
            ArrowData data = serializer.Deserialize(new JsonTextReader(sr), typeof(ArrowData)) as ArrowData;

            if(gameObject.name == "LeaderPanel")
            {
                manager.StartPanel("AlwaysPanel");
            }

            StartCoroutine(TestAddArrow(data));
        }

        IEnumerator TestAddArrow(ArrowData data)
        {
            int preTime = data.basetime;
            for (int i = 0; i < data.beats.Count; ++i)
            {
                yield return new WaitForSeconds((data.beats[i].time - preTime) / 1000.0f - 0.3f);

                SceneManager.dancer.DoDrum(data.beats[i].type);

                yield return new WaitForSeconds(0.3f);
                arrowManager.AddArrow(data.beats[i].type);
                preTime = data.beats[i].time;
            }

            yield return new WaitForSeconds(0.3f);

            SceneManager.dancer.DoYourTurn(true);
        }
    }
}
