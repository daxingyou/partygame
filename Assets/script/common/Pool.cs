/*************************************************************
   Copyright(C) 2017 by dayugame
   All rights reserved.
   
   Pool.cs
   PartyRhythmGame
   
   Created by WuIslet on 2018-01-16.
   
*************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace isletspace
{
    /// <summary>
    /// 
    /// </summary>
    public static class Pool
    {
        public static GameObject CreateObject(string name, Transform parent)
        {
            return CreateObject(name, parent, Vector3.zero, Quaternion.identity);
        }

        public static GameObject CreateObject(string name, Transform parent, Vector3 pos)
        {
            return CreateObject(name, parent, pos, Quaternion.identity);
        }

        public static GameObject CreateObject(string name, Transform parent, Vector3 pos, Quaternion rotate)
        {
            var prefab = Resources.Load("prefab/" + name) as GameObject;
            var obj = GameObject.Instantiate(prefab, pos, rotate, parent) as GameObject;
            obj.transform.localPosition = pos;
            return obj;
        }
    }
}
