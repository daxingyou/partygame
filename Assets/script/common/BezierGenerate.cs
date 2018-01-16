/*************************************************************
   Copyright(C) 2017 by dayugame
   All rights reserved.
   
   BezierGenerate.cs
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
    public static class BezierGenerate
    {
        public static Vector3 CreatePoint(Vector3 beginPos, Vector3 controlPos, Vector3 endPos, float ratio)
        {
            float tmp = 1 - ratio;
            return beginPos * tmp * tmp + 2 * controlPos * ratio * tmp + endPos * ratio * ratio;
        }
        
        public static Vector3 CreatePoint(Vector3 beginPos, Vector3 controlPos01, Vector3 controlPos02, Vector3 endPos, float ratio)
        {
            float tmp = 1 - ratio;
            return beginPos * Mathf.Pow(tmp, 3) + 3 * controlPos01 * ratio * Mathf.Pow(tmp, 2) + 3 * controlPos02 * Mathf.Pow(ratio, 2) * tmp + endPos * Mathf.Pow(ratio, 3);
        }
    }
}
