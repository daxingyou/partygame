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

        public void PreLoad(string name, string url)
        {
            nameObj = Utils.FindDirectChildComponent<Text>("name", transform);
            nameObj.text = name;
            headimg = Utils.FindDirectChildComponent<IconManager>("headimg", transform);
            headimg.SetFace(url);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
