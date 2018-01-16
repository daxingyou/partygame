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

        private void Start()
        {
            currentPanel = FindChildPanel("StartPanel");
            currentPanel.DoStart(this);
        }

        public void ClickStart()
        {
            //JumpTo("OPPanel");
            //JumpTo("RankPanel");
            net.StartNet();
        }

        public void OnPanelOver(string name, string next)
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

        private void JumpTo(string name)
        {
            var next = FindChildPanel(name);
            if (next == null)
            {
                next = FindChildPanel("OPPanel");
            }

            currentPanel.DoEnd();
            next.DoStart(this);
            currentPanel = next;
        }

        private PanelBase FindChildPanel(string name)
        {
            var child = panelParent.Find(name);
            if (child == null)
            {
                return null;
            }

            return child.gameObject.GetComponent<PanelBase>();
        }
    }
}
