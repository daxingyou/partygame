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
        public NetManager net;
        public Director director;
        public AllDancerManager allDancerManager;

        private PanelBase currentPanel;

        private void Start()
        {
            StartPanelAsCurrent("StartPanel");
        }

        public void ClickStart()
        {
            JumpTo("OPPanel");
            //JumpTo("RankPanel");
            //JumpTo("TrainLeaderPanel");
            //net.StartNet();
        }

        public void OnPanelOver(string name, string next, string camera)
        {
            if(!string.IsNullOrEmpty(next))
            {
                JumpTo(next);
            }

            switch (name)
            {
                case "OPPanel":
                    break;
            }
        }

        public void JumpTo(string name)
        {
            currentPanel.DoEnd();
            StartPanelAsCurrent(name);
        }

        public void StartPanel(string name)
        {
            var panel = Utils.FindDirectChildComponent<PanelBase>(name, panelParent);
            if (panel == null)
            {
                return;
            }
            panel.DoStart(this);
        }

        private void StartPanelAsCurrent(string name)
        {
            var panel = Utils.FindDirectChildComponent<PanelBase>(name, panelParent);
            if (panel == null)
            {
                panel = Utils.FindDirectChildComponent<PanelBase>("OPPanel", panelParent);
            }

            panel.DoStart(this);
            string camera = panel.cameraScene;
            if (!string.IsNullOrEmpty(camera))
            {
                director.ChangeCamera(camera);
            }
            currentPanel = panel;
        }

        public void EndPanel(string name)
        {
            var panel = Utils.FindDirectChildComponent<PanelBase>(name, panelParent);
            if (panel == null)
            {
                return;
            }
            panel.DoEnd();
        }
    }
}
