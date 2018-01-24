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
        [Tooltip("For Debug")]
        public bool isForceLoop;

        public List<NextPanel> nextPanelOrder; //TODO  想用栈结构储存，但是注意是先进先出的，要用轻量级队列。
        public string cameraScene = "";

        [Tooltip("For Debug")]
        public float currentTimeOut;
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

            SetTimeOut(nextPanelOrder[l].timeout);
        }

        protected void SetTimeOut(float timeout) //TODO 时长的修改没有刷新对应方法，应该要做成注册形式。
        {
            CancelInvoke("TimeOut");
            currentTimeOut = timeout;
            if(timeout > 0)
            {
                Invoke("TimeOut", currentTimeOut);
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
