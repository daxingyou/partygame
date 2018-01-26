/*************************************************************
   Copyright(C) 2017 by dayugame
   All rights reserved.
   
   CloseUpUI.cs
   PartyRhythmGame
   
   Created by WuIslet on 2018-01-19.
   
*************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace isletspace
{
    /// <summary>
    /// 
    /// </summary>
    public class CloseUpUI : MonoBehaviour
    {
        public Text nameObj;
        public IconManager headimg;

        public void PreLoad()
        {
            //Hide();
            nameObj = Utils.FindDirectChildComponent<Text>("name", transform);
            nameObj.text = GameManager.GetMVPName();
            headimg = Utils.FindDirectChildComponent<IconManager>("headimg", transform);
            headimg.SetFace(GameManager.GetMVPUrl());
        }

        public void Show()
        {
            transform.localPosition = Vector3.forward * 10;
        }

        public void Hide()
        {
            transform.localPosition = Vector3.forward * -650;
        }
    }
}
