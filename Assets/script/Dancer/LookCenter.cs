/*************************************************************
   Copyright(C) 2017 by dayugame
   All rights reserved.
   
   LookCenter.cs
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
    [ExecuteInEditMode]
    public class LookCenter : MonoBehaviour
    {
        private void Start()
        {
            Utils.ChildLook(transform);
        }
    }
}
