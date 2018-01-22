/*************************************************************
   Copyright(C) 2017 by dayugame
   All rights reserved.
   
   SettlePanel.cs
   PartyRhythmGame
   
   Created by WuIslet on 2018-01-17.
   
*************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace isletspace
{
    /// <summary>
    /// 
    /// </summary>
    public class SettlePanel : PanelBase
    {
        public AllDancerManager SceneManager;

        public CloseUpUI closeUp;

        private Vector3 target;

        public override void DoStart(UIManager manager)
        {
            base.DoStart(manager);
            SceneManager.AllDrumConstant();
            PickShow();
            Invoke("DoCloseUp", 0.5f);
        }

        public void PickShow()
        {
            closeUp.PreLoad("1234", "http://wx.qlogo.cn/mmopen/vi_32/icvxBfeXY9WGXGhjE7ELzuBCQKxLu4laWkXYtRROIdxT8UXZPQmfREIE3VFXc7Krib8oREiclGC8QicZP0fCqcAYRw/132");
        }

        public void DoCloseUp()
        {
            int pos = 3;
            target = SceneManager.CloseUp(pos);
            Director.Instance.RegistChangeCamera("AllDancerScene", OnAllDancerCloseUp);
            closeUp.Show();
        }

        private void OnAllDancerCloseUp(SceneCamera camera)
        {
            camera.AllDancerCloseUp(target);
            Director.Instance.UnRegistChangeCamera(OnAllDancerCloseUp);
        }
    }
}
