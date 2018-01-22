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
using DG.Tweening;

namespace isletspace
{
    [System.Serializable]
    public class NextPanel
    {
        public float timeout;
        public string nextName;

        public NextPanel(string name, float time)
        {
            timeout = time;
            nextName = name;
        }
        
        public NextPanel(string name, string time)
        {
            timeout = float.Parse(time);
            nextName = name;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class PanelBase : MonoBehaviour
    {
        public bool isForceLoop;

        public List<NextPanel> nextPanelOrder;
        public string cameraScene = "";

        public float currentTime;
        public Vector2 OriginPos;

        [System.NonSerialized]
        public UIManager manager;
        

        virtual public void DoStart(UIManager manager)
        {
            this.manager = manager;
            gameObject.SetActive(true);

            if(OriginPos != null)
            {
                transform.DOLocalMove(Vector3.zero, 1);
            }

            var l = nextPanelOrder.Count - 1;
            if(l < 0)
            {
                return;
            }
            currentTime = nextPanelOrder[l].timeout;
            if (currentTime > 0)
            {
                Invoke("TimeOut", currentTime);
            }
        }

        virtual public void DoEnd()
        {
            gameObject.SetActive(false);

            transform.localPosition = OriginPos;
            /*  //TODO  要不要做移动玩了再SetActive(False)？
            if (OriginPos != null)
            {
                transform.DOLocalMove(OriginPos, 1);
            }*/
        }

        virtual public void TimeOut()
        {
            if (isForceLoop)
            {
                return;
            }
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
            manager.OnPanelOver(gameObject.name, nextPanelOrder[l]);
            if (l != 0)
            {
                nextPanelOrder.RemoveAt(l);
            }
        }
    }
}
