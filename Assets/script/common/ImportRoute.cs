/*************************************************************
   Copyright(C) 2017 by dayugame
   All rights reserved.
   
   ImportRoute.cs
   PartyRhythmGame
   
   Created by WuIslet on 2018-01-18.
   
*************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

namespace isletspace
{
    public class OnePhase
    {
        public string beat;
        public string time;
        public string beattime;
    }

    public class RouteData
    {
        public List<OnePhase> phase0;
        public List<OnePhase> phase1;
        public List<OnePhase> phase2;
        public List<OnePhase> phase3;
    }

    /// <summary>
    /// 
    /// </summary>
    public class ImportRoute : MonoBehaviour
    {
        public Transform panelParent;

        public static RouteData data;
        private void Start()
        {
            StartCoroutine(getText());
        }

        IEnumerator getText()
        {
            var www = new WWW(Utils.GetStreamingPath() + "Route.json");
            yield return www;
            Decode(www.text);
            SetNextPanel();
        }

        private void Decode(string json)
        {
            print(json);

            JsonSerializer serializer = new JsonSerializer();
            StringReader sr = new StringReader(json);
            data = serializer.Deserialize(new JsonTextReader(sr), typeof(RouteData)) as RouteData;
        }

        private void SetNextPanel()
        {
            PanelBase panel = null;

            for (int i = 0; i < data.phase0.Count; ++i)
            {
                string endphase = i == (data.phase0.Count - 1) ? "ReadyPanel" : "TrainLeaderPanel";

                string[] substring = data.phase0[i].time.Split(new char[] { ',' });

                panel = Utils.FindDirectChildComponent<PanelBase>("TrainLeaderPanel", panelParent);
                panel.nextPanelOrder.Add(new NextPanel("TrainPlayTurnPanel", substring[0]));

                if (substring.Length == 2 || substring[2] == "0")
                {
                    panel = Utils.FindDirectChildComponent<PanelBase>("TrainPlayTurnPanel", panelParent);
                    panel.nextPanelOrder.Add(new NextPanel(endphase, substring[1]));
                }
                else if (substring.Length == 3)
                {
                    panel = Utils.FindDirectChildComponent<PanelBase>("TrainPlayTurnPanel", panelParent);
                    panel.nextPanelOrder.Add(new NextPanel("TrainSettlePanel", substring[1]));

                    panel = Utils.FindDirectChildComponent<PanelBase>("TrainSettlePanel", panelParent);
                    panel.nextPanelOrder.Add(new NextPanel(endphase, substring[2]));
                }
            }

            for (int i = 0; i < data.phase1.Count; ++i)
            {
                string endphase = i == (data.phase1.Count - 1) ? "CheerPanel" : "LeaderPanel";

                string[] substring = data.phase1[i].time.Split(new char[] { ',' });

                panel = Utils.FindDirectChildComponent<PanelBase>("LeaderPanel", panelParent);
                panel.nextPanelOrder.Add(new NextPanel("PlayTurnPanel", substring[0]));

                if(substring.Length == 2 || substring[2] == "0")
                {
                    panel = Utils.FindDirectChildComponent<PanelBase>("PlayTurnPanel", panelParent);
                    panel.nextPanelOrder.Add(new NextPanel(endphase, substring[1]));
                }
                else if(substring.Length == 3)
                {
                    panel = Utils.FindDirectChildComponent<PanelBase>("PlayTurnPanel", panelParent);
                    panel.nextPanelOrder.Add(new NextPanel("SettlePanel", substring[1]));

                    panel = Utils.FindDirectChildComponent<PanelBase>("SettlePanel", panelParent);
                    panel.nextPanelOrder.Add(new NextPanel(endphase, substring[2]));
                }
            }

            for (int i = 0; i < data.phase2.Count; ++i)
            {
                string endphase = i == (data.phase2.Count - 1) ? "CheerPanel" : "LeaderPanel";

                string[] substring = data.phase2[i].time.Split(new char[] { ',' });

                panel = Utils.FindDirectChildComponent<PanelBase>("LeaderPanel", panelParent);
                panel.nextPanelOrder.Add(new NextPanel("PlayTurnPanel", substring[0]));

                if (substring.Length == 2 || substring[2] == "0")
                {
                    panel = Utils.FindDirectChildComponent<PanelBase>("PlayTurnPanel", panelParent);
                    panel.nextPanelOrder.Add(new NextPanel(endphase, substring[1]));
                }
                else if (substring.Length == 3)
                {
                    panel = Utils.FindDirectChildComponent<PanelBase>("PlayTurnPanel", panelParent);
                    panel.nextPanelOrder.Add(new NextPanel("SettlePanel", substring[1]));

                    panel = Utils.FindDirectChildComponent<PanelBase>("SettlePanel", panelParent);
                    panel.nextPanelOrder.Add(new NextPanel(endphase, substring[2]));
                }
            }

            for (int i = 0; i < data.phase3.Count; ++i)
            {
                string endphase = i == (data.phase3.Count - 1) ? "RankPanel" : "LeaderPanel";

                string[] substring = data.phase3[i].time.Split(new char[] { ',' });

                panel = Utils.FindDirectChildComponent<PanelBase>("LeaderPanel", panelParent);
                panel.nextPanelOrder.Add(new NextPanel("PlayTurnPanel", substring[0]));

                if (substring.Length == 2 || substring[2] == "0")
                {
                    panel = Utils.FindDirectChildComponent<PanelBase>("PlayTurnPanel", panelParent);
                    panel.nextPanelOrder.Add(new NextPanel(endphase, substring[1]));
                }
                else if (substring.Length == 3)
                {
                    panel = Utils.FindDirectChildComponent<PanelBase>("PlayTurnPanel", panelParent);
                    panel.nextPanelOrder.Add(new NextPanel("SettlePanel", substring[1]));

                    panel = Utils.FindDirectChildComponent<PanelBase>("SettlePanel", panelParent);
                    panel.nextPanelOrder.Add(new NextPanel(endphase, substring[2]));
                }
            }

            panel = Utils.FindDirectChildComponent<PanelBase>("TrainLeaderPanel", panelParent);
            panel.nextPanelOrder.Reverse();
            panel = Utils.FindDirectChildComponent<PanelBase>("TrainPlayTurnPanel", panelParent);
            panel.nextPanelOrder.Reverse();
            panel = Utils.FindDirectChildComponent<PanelBase>("TrainSettlePanel", panelParent);
            panel.nextPanelOrder.Reverse();
            panel = Utils.FindDirectChildComponent<PanelBase>("LeaderPanel", panelParent);
            panel.nextPanelOrder.Reverse();
            panel = Utils.FindDirectChildComponent<PanelBase>("PlayTurnPanel", panelParent);
            panel.nextPanelOrder.Reverse();
            panel = Utils.FindDirectChildComponent<PanelBase>("SettlePanel", panelParent);
            panel.nextPanelOrder.Reverse();
        }

        public static List<int> GetBeat()
        {
            int phase = GameManager.gamePhase;
            int time = GameManager.phaseTime;
            string result = "";
            switch (phase)
            {
                case 99:
                    result = data.phase0[time].beat;
                    break;
                case 0:
                    result = data.phase1[time].beat;
                    break;
                case 1:
                    result = data.phase2[time].beat;
                    break;
                case 2:
                    result = data.phase3[time].beat;
                    break;
            }
            return Str2List(result);
        }

        public static List<int> GetBeatTime()
        {
            int phase = GameManager.gamePhase;
            int time = GameManager.phaseTime;
            string result = "";
            switch (phase)
            {
                case 99:
                    result = data.phase0[time].beattime;
                    break;
                case 0:
                    result = data.phase1[time].beattime;
                    break;
                case 1:
                    result = data.phase2[time].beattime;
                    break;
                case 2:
                    result = data.phase3[time].beattime;
                    break;
            }
            return Str2List(result);
        }
        
        public static List<int> Str2List(string data)
        {
            List<int> result = new List<int>();

            string[] subString = data.Split(new char[] { ',' });
            for (int i = 0; i < subString.Length; ++i)
            {
                result.Add(int.Parse(subString[i]));
            }

            return result;
        }
    }
}
