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
        [Header("测试用版面。除非知道，否则不要随便设置")]
        public bool EnableDebugPanel = true;
        [ConditionalHide("EnableDebugPanel", true)]
        public bool isForceLoop;
        [ConditionalHide("EnableDebugPanel", true)]
        public float currentTimeOut;
        
        [Header("MovePanel")]
        public bool EnableMovePanel = true;
        [ConditionalHide("EnableMovePanel", true)]
        public Vector3 OriginPos;
        [Header("--------------------")]

        public List<NextPanel> nextPanelOrder; //TODO  想用栈结构储存，但是注意是先进先出的，要用轻量级队列。
        public string cameraScene = "";

        [System.NonSerialized]
        public UIManager manager;
        

        public virtual void DoStart(UIManager manager)
        {
            this.manager = manager;
            gameObject.SetActive(true);

            if(EnableMovePanel)
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

        public virtual void DoEnd()
        {
            if (EnableMovePanel) //移动的版面不隐藏
            {
                transform.localPosition = OriginPos;
            }
            else
            {
                gameObject.SetActive(false);
            }
        }

        public virtual void TimeOut()
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
