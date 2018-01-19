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
        public List<beatData> beattime;
    }

    /// <summary>
    /// 
    /// </summary>
    public class LeaderPanel : PanelBase
    {
        public LeaderManager SceneManager;
        public ArrowManager arrowManager;

        public override void DoStart(UIManager manager)
        {
            base.DoStart(manager);
            if (gameObject.name == "LeaderPanel")
            {
                manager.StartPanel("AlwaysPanel");
            }

            print("   start   play   " + GameManager.gamePhase + " , " + GameManager.phaseTime);
            print("    play  args   " + ImportRoute.GetBeat() + " , " + ImportRoute.GetBeatTime());
            StartCoroutine(TestAddArrow(Str2List(ImportRoute.GetBeat()), Str2List(ImportRoute.GetBeatTime())));

            GameManager.phaseTime += 1;
        }

        public override void DoEnd()
        {
            base.DoEnd();
            SceneManager.dancer.DoYourTurn(false);
        }

        IEnumerator TestAddArrow(List<int> beat, List<int> beattime)
        {
            arrowManager.ClearList();

            int preTime = 0;
            for (int i = 0; i < beat.Count; ++i)
            {
                yield return new WaitForSeconds((beattime[i] - preTime) / 1000.0f - 0.3f);

                SceneManager.dancer.DoDrum(beat[i]);

                yield return new WaitForSeconds(0.3f);
                arrowManager.AddArrow(beat[i]);
                preTime = beattime[i];
            }

            yield return new WaitForSeconds(0.3f);

            SceneManager.dancer.DoYourTurn(true);
        }

        private List<int> Str2List(string data)
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
