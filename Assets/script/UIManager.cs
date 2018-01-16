/*************************************************************
   Copyright(C) 2017 by dayugame
   All rights reserved.
   
   UIManager.cs
   PartyRhythmGame
   
   Created by WuIslet on 2018-01-10.
   
*************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft;

namespace isletspace
{
    /// <summary>
    /// 
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        public Transform panelParent;
        public PanelBase currentPanel;
        public NetManager net;
        public Director director;

        private void Start()
        {
            currentPanel = Utils.FindDirectChildComponent<PanelBase>("StartPanel", panelParent);
            currentPanel.DoStart(this);
        }

        public void ClickStart()
        {
            //JumpTo("OPPanel");
            //JumpTo("RankPanel");
            //JumpTo("TrainLeaderPanel");
            net.StartNet();
        }

        public void OnPanelOver(string name, string next, string camera)
        {
            if(!string.IsNullOrEmpty(next))
            {
                JumpTo(next);
            }
            
            if(!string.IsNullOrEmpty(camera))
            {
                director.ChangeCamera(camera);
            }

            switch (name)
            {
                case "OPPanel":
                    break;
            }
        }

        private void JumpTo(string name)
        {
            var next = Utils.FindDirectChildComponent<PanelBase>(name, panelParent);
            if (next == null)
            {
                next = Utils.FindDirectChildComponent<PanelBase>("OPPanel", panelParent);
            }

            currentPanel.DoEnd();
            next.DoStart(this);
            currentPanel = next;
        }
    }
}
