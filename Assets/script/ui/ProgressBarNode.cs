/*************************************************************
   Copyright(C) 2017 by dayugame
   All rights reserved.
   
   ProgressBarNode.cs
   PartyRhythmGame
   
   Created by WuIslet on 2018-01-22.
   
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
    public class ProgressBarNode : MonoBehaviour
    {
        bool isBoom;
        public void NodeActive(int no)
        {
            isBoom = true;
            for (int i = 0; i < transform.childCount; ++i)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }

            var fire = gameObject.GetComponent<Image>();
            fire.sprite = Resources.Load("Textures/img_fire_0" + no, typeof(Sprite)) as Sprite;
        }

        private void OnDisable()
        {
            if (isBoom)
            {
                transform.Find("baozha").gameObject.SetActive(false);
            }
        }
    }
}
