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
    /// <summary>
    /// 
    /// </summary>
    public class ArrowManager : MonoBehaviour
    {
        public Transform List;
        public DancerAni dancer;

        public void AddArrow(int type)
        {
            string path = "";
            switch (type)
            {
                case 1:
                    path = "prefab/Arrow/A";
                    break;
                case 2:
                    path = "prefab/Arrow/B";
                    break;
                case 3:
                    path = "prefab/Arrow/AB";
                    break;
                default:
                    return;
            }
            var prefab = Resources.Load(path) as GameObject;
            var obj = Instantiate(prefab, Vector3.one, Quaternion.identity, List) as GameObject;
            if(dancer != null)
            {
                dancer.DoAction(type);
            }
        }
    }
}
