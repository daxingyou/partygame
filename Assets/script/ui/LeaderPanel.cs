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
                //manager.StartPanel("AlwaysPanel");
                if(GameManager.gamePhase < 5)
                    SoundManager.Instance.PlayBackground(GameManager.gamePhase);
            }

            print("   start   play   " + GameManager.gamePhase + " , " + GameManager.phaseTime);

            arrowManager.ClearList();
            StartCoroutine(PlayDrumList(ImportRoute.GetBeat(), ImportRoute.GetBeatTime()));
        }

        public override void DoEnd()
        {
            base.DoEnd();
            SceneManager.dancer.DoYourTurn(false);
        }

        IEnumerator PlayDrumList(List<int> beat, List<int> beattime)
        {
            yield return StartCoroutine(SceneManager.dancer.DrumList(beat, beattime, arrowManager.AddArrow));
            
            yield return new WaitForSeconds(0.3f);
            SceneManager.dancer.DoYourTurn(true);
        }
    }
}
