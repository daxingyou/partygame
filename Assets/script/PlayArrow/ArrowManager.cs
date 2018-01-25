/*************************************************************
   Copyright(C) 2017 by dayugame
   All rights reserved.
   
   ArrowManager.cs
   PartyRhythmGame
   
   Created by WuIslet on 2018-01-10.
   
*************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace isletspace
{
    public delegate void AddCallback(int downidx, int type);

    /// <summary>
    /// 
    /// </summary>
    public class ArrowManager : MonoBehaviour
    {
        public Transform List;

        public void AddArrow(int downidx, int type) //预留参数downidx用来倒计时。
        {
            string path = "";
            switch (type)
            {
                case 1:
                    path = "Arrow/A";
                    break;
                case 2:
                    path = "Arrow/B";
                    break;
                case 3:
                    path = "Arrow/AB";
                    break;
                default:
                    return;
            }
            var arrow = Pool.CreateObject(path, List);
        }

        public void ClearList()
        {
            for (int i = List.childCount - 1; i >= 0; --i)
            {
                Destroy(List.GetChild(i).gameObject);
            }
        }
    }
}
