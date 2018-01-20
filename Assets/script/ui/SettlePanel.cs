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

        public override void DoStart(UIManager manager)
        {
            base.DoStart(manager);
            if (gameObject.name == "SettlePanel")
            {
                closeUp.PreLoad("1234", "4567");
                Invoke("DoCloseUp", 3);
            }
        }

        public void DoCloseUp()
        {
            int pos = 3;
            Vector3 target = SceneManager.CloseUp(pos);
            Director.Instance.DoAllDancerCloseUp(target);
            closeUp.Show();
        }
    }
}
