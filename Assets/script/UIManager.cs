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
        public AllDancerManager allDancerManager;

        public string jumpPanelName;

        private PanelBase currentPanel;

        private void Start()
        {
            StartPanelAsCurrent("StartPanel");
        }

        public void ClickStart()
        {
            if (string.IsNullOrEmpty(jumpPanelName))
            {
                JumpTo("OPPanel");
            }
            else
            {
                JumpTo(jumpPanelName);
            }
            SoundManager.Instance.PlaySingleA();
            NetManager.Instance.StartNet();
        }

        public void OnPanelOver(string name, NextPanel panel)
        {
            if(panel != null)
            {
                JumpTo(panel.nextName);
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
                Director.Instance.ChangeCamera(camera);
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
