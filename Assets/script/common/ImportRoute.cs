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
            var www = new WWW(Utils.GetStreamingPath() + "SoloRoute.json");
            yield return www;
            SetSoloList(www.text);

            www = new WWW(Utils.GetStreamingPath() + "Route.json");
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
                string endphase = i == (data.phase2.Count - 1) ? "SecondCheerPanel" : "LeaderPanel";

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

        private void SetSoloList(string json)
        {
            print(json);

            JsonSerializer serializer = new JsonSerializer();
            StringReader sr = new StringReader(json);
            GameManager.soloList = serializer.Deserialize(new JsonTextReader(sr), typeof(List<float>)) as List<float>;
        }

        public static List<int> GetBeat()
        {
            int time = GameManager.phaseTime;
            var list = getPhase();
            return Str2List(list[time].beat);
        }

        public static List<int> GetBeatTime()
        {
            int time = GameManager.phaseTime;
            var list = getPhase();
            return Str2List(list[time].beattime);
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

        public static List<OnePhase> getPhase()
        {
            int phase = GameManager.gamePhase;
            switch (phase)
            {
                case 99:
                    return data.phase0;
                case 0:
                    return data.phase1;
                case 1:
                    return data.phase2;
                case 2:
                    return data.phase3;
                default:
                    return null;
            }
        }
    }
}
