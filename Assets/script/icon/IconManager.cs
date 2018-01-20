/*************************************************************
   Copyright(C) 2017 by dayugame
   All rights reserved.
   
   IconManager.cs
   PartyRhythmGame
   
   Created by WuIslet on 2018-01-20.
   
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
    [RequireComponent(typeof(Image))]
    public class IconManager : MonoBehaviour
    {
        public void SetFace(string playerHeadImg)
        {
            var image = gameObject.GetComponent<Image>();

            if (playerHeadImg.StartsWith("http"))
            {
                ImageExtends wwwImage = image.GetComponent<ImageExtends>();
                if (null == wwwImage)
                {
                    wwwImage = image.gameObject.AddComponent<ImageExtends>();
                }
                wwwImage.SetSprite(image, playerHeadImg);
            }
            else
            {
                image.sprite = Resources.Load("img/headimg_default", typeof(Sprite)) as Sprite;
            }
        }
    }
}
