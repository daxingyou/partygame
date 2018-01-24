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
            //manager.StartPanel("AlwaysPanel");
            if(GameManager.gamePhase > 10) //保险
            {
                GameManager.gamePhase = 0;
            }

            SoundManager.Instance.PlayBackground(GameManager.gamePhase);
            arrowManager.ClearList();
            float startTime = 0;
            if(GameManager.phaseTime == 0) //第一回合额外片头时间
            {
                SetTimeOut(currentTimeOut + GameManager.PHASE_START_DELAY[GameManager.gamePhase]);
                startTime = GameManager.PHASE_START_DELAY[GameManager.gamePhase];
            }
            StartCoroutine(PlayDrumList(startTime, ImportRoute.GetBeat(), ImportRoute.GetBeatTime()));
        }

        public override void DoEnd()
        {
            base.DoEnd();
            SceneManager.dancer.DoYourTurn(false);
        }

        IEnumerator PlayDrumList(float startDelay, List<int> beat, List<int> beattime)
        {
            if(startDelay > 0)
            {
                yield return new WaitForSeconds(startDelay);
            }
            yield return StartCoroutine(SceneManager.dancer.DrumList(beat, beattime, arrowManager.AddArrow));
            
            yield return new WaitForSeconds(0.3f);
            SceneManager.dancer.DoYourTurn(true);
        }
    }
}
