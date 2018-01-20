/*************************************************************
   Copyright(C) 2017 by dayugame
   All rights reserved.
   
   TrainLeaderPanel.cs
   PartyRhythmGame
   
   Created by WuIslet on 2018-01-20.
   
*************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace isletspace
{
    /// <summary>
    /// 
    /// </summary>
    public class TrainLeaderPanel : PanelBase
    {
        public LeaderManager SceneManager;
        public ArrowManager arrowManager;

        public override void DoStart(UIManager manager)
        {
            GameManager.gamePhase = 99;
            base.DoStart(manager);
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
