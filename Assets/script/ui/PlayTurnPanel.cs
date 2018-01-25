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
        public ProgressBar bar; //TODO ���������Ƿ���AllwaysPanel��
        public List<GameObject> SpriteAniList;
        public CloseUpUI closeUp;

        int lightSpotCnt;

        public override void DoStart(UIManager manager)
        {
            base.DoStart(manager);
            
            TryDelayIfLastPanel();
            
            SceneManager.AllDrumList();
            var startLightTime = currentTimeOut - 3; //3�ǹ��������Ҫ��ʱ�䡣���ȱ�֤�����ܷ���ɡ�
            startLightTime = Mathf.Clamp(startLightTime, 0, ImportRoute.GetBeatTime().Sum() / 1000f); //����ʼʱ�������ںϷ���Χ�ڡ�

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
        /// �����һ��������CheerPanel������Ϊ����׶������һ���׶Σ�����׷�������벥��ת��������
        /// </summary>
        private void TryDelayIfLastPanel()
        {
            var l = nextPanelOrder.Count - 1;
            if (l < 0)
            {
                return;
            }

            if (nextPanelOrder[l].nextName == "CheerPanel" || nextPanelOrder[l].nextName == "SecondCheerPanel" || nextPanelOrder[l].nextName == "EDPanel") //TODO  ��ʱ�����жϣ�����׼ȷ��
            {
                StartCoroutine(ChangePanelRoute(currentTimeOut));
                SetTimeOut(currentTimeOut + 7f);
            }
        }

        private IEnumerator ChangePanelRoute(float time)
        {
            yield return new WaitForSeconds(time);           //�ȴ��������̽���

            Director.Instance.currentCamera.CancelRandomJumpCamera("MainCamera");  //�رվ�ͷ����л�

            //TODO  ������°��������ProgressBar.AniRoute������ˡ����һ��������������һ��ͳһ��Я�̡�
        }

        private void TryPreLoadIfNextSettle()
        {
            var l = nextPanelOrder.Count - 1;
            if (l < 0)
            {
                return;
            }

            if (nextPanelOrder[l].nextName == "SettlePanel") //TODO  ��ʱ�����жϣ�����׼ȷ��
            {
                GameManager.PickMVP();
                closeUp.PreLoad();
            }
        }
    }
}
