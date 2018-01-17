/*************************************************************
   Copyright(C) 2017 by dayugame
   All rights reserved.
   
   PanelBase.cs
   PartyRhythmGame
   
   Created by WuIslet on 2018-01-11.
   
*************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace isletspace
{
    /// <summary>
    /// 
    /// </summary>
    public class PanelBase : MonoBehaviour
    {
        public float timeout = -1;
        public List<string> nextPanelOrder;
        public string cameraScene = "";

        [System.NonSerialized]
        public UIManager manager;
        

        virtual public void DoStart(UIManager manager)
        {
            this.manager = manager;
            gameObject.SetActive(true);

            if(timeout > 0)
            {
                Invoke("TimeOut", timeout);
            }
        }

        virtual public void DoEnd()
        {
            gameObject.SetActive(false);
        }

        virtual public void TimeOut()
        {
            CancelInvoke("TimeOut");
            Over();
        }

        private void Over()
        {
            int l = nextPanelOrder.Count - 1;
            if(l < 0)
            {
                return;
            }
            manager.OnPanelOver(gameObject.name, nextPanelOrder[l], cameraScene);
            if (l != 0)
            {
                nextPanelOrder.RemoveAt(l);
            }
        }
    }
}
