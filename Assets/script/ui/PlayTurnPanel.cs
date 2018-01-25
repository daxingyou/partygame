/*************************************************************
   Copyright(C) 2017 by dayugame
   All rights reserved.
   
   PlayTurnPanel.cs
   PartyRhythmGame
   
   Created by WuIslet on 2018-01-17.
   
*************************************************************/

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace isletspace
{
    /// <summary>
    /// 
    /// </summary>
    public class PlayTurnPanel : PanelBase
    {
        public AllDancerManager SceneManager;
        public ProgressBar bar; //TODO 进度条考虑放入AllwaysPanel。
        public List<GameObject> SpriteAniList;
        public CloseUpUI closeUp;

        int lightSpotCnt;

        public override void DoStart(UIManager manager)
        {
            base.DoStart(manager);
            
            TryDelayIfLastPanel();
            
            SceneManager.AllDrumList();
            var startLightTime = currentTimeOut - 3; //3是光球飞行需要的时间。优先保证光球能飞完成。
            startLightTime = Mathf.Clamp(startLightTime, 0, ImportRoute.GetBeatTime().Sum() / 1000f); //将起始时间限制在合法范围内。

            Invoke("PlayLightSpotAll", startLightTime);

            GameManager.phaseTime += 1;
        }

        public override void DoEnd()
        {
            base.DoEnd();
            for (int i = 0; i < SpriteAniList.Count; ++i)
            {
                SpriteAniList[i].GetComponent<Animator>().Stop();
                SpriteAniList[i].SetActive(false);
            }
        }

        public void PlayLightSpotAll()
        {
            lightSpotCnt = SceneManager.PlayLightSpotAll(LightSpotCallback);
        }

        public void LightSpotCallback()
        {
            float interval = 1f / 3 / ImportRoute.getPhase().Count / lightSpotCnt;
            bar.AddProgress(interval);
        }

        /// <summary>
        /// 如果下一个场景是CheerPanel。则认为这个阶段是最后一个阶段，额外追加若干秒播放转场动画。
        /// </summary>
        private void TryDelayIfLastPanel()
        {
            var l = nextPanelOrder.Count - 1;
            if (l < 0)
            {
                return;
            }

            if (nextPanelOrder[l].nextName == "CheerPanel" || nextPanelOrder[l].nextName == "SecondCheerPanel" || nextPanelOrder[l].nextName == "EDPanel") //TODO  暂时这样判断，不够准确。
            {
                StartCoroutine(ChangePanelRoute(currentTimeOut));
                SetTimeOut(currentTimeOut + 7f);
            }
        }

        private IEnumerator ChangePanelRoute(float time)
        {
            yield return new WaitForSeconds(time);           //等待正常流程结束

            Director.Instance.currentCamera.CancelRandomJumpCamera("MainCamera");  //关闭镜头随机切换

            //TODO  这里的下半段流程在ProgressBar.AniRoute里完成了。最好一整个流程能做成一个统一的携程。
        }

        private void TryPreLoadIfNextSettle()
        {
            var l = nextPanelOrder.Count - 1;
            if (l < 0)
            {
                return;
            }

            if (nextPanelOrder[l].nextName == "SettlePanel") //TODO  暂时这样判断，不够准确。
            {
                GameManager.PickMVP();
                closeUp.PreLoad();
            }
        }
    }
}
