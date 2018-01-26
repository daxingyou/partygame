/*************************************************************
   Copyright(C) 2017 by dayugame
   All rights reserved.
   
   PoseLightManager.cs
   PartyRhythmGame
   
   Created by WuIslet on 2018-01-26.
   
*************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace isletspace
{
    /// <summary>
    /// 
    /// </summary>
    public class PoseLightManager : MonoBehaviour
    {
        private bool isPoseLight;
        private SkinnedMeshRenderer smr;
        private Material[] oldMaterial;

        private void Start()
        {
            //MeshRenderer smr = gameObject.GetComponent<MeshRenderer>();
            smr = gameObject.GetComponent<SkinnedMeshRenderer>();
        }

        public void Begin()
        {
            isPoseLight = true;
            oldMaterial = smr.materials;
            var mat1 = Resources.Load("model/Materials/jing") as Material;
            mat = Resources.Load("model/Materials/flash") as Material;
            Material[] newMat = { mat1, mat };
            smr.materials = newMat;
        }

        public void End() //TODO 为什么没有变回去？目前先无视。
        {
            smr.materials = oldMaterial;
            isPoseLight = false;
        }


        #region 流光特效
        private Material mat;

        /// <summary>
        /// 循环一次所用时间
        /// </summary>
        private float loopSecond = 4f;

        /// <summary>
        /// 循环的最小值
        /// </summary>
        private float minValuex = 0;

        /// <summary>
        /// 循环的最大值
        /// </summary>
        private float maxValuex = 1;


        /// <summary>
        /// 循环的最小值
        /// </summary>
        private float minValuey = 0;

        /// <summary>
        /// 循环的最大值
        /// </summary>
        private float maxValuey = 1;

        DateTime lastTime;

        // Update is called once per frame
        void LateUpdate()
        {
            if (isPoseLight)
            {
                float valuex = mat.mainTextureOffset.x;
                float valuey = mat.mainTextureOffset.y;

                float second = (float) (DateTime.Now - lastTime).TotalSeconds;
                lastTime = DateTime.Now;
                float bilix = (maxValuex - minValuex)*(second/loopSecond);
                float biliy = (maxValuey - minValuey)*(second/loopSecond);
                valuex += bilix;
                valuey += biliy;
                if (valuex > maxValuex)
                    valuex = minValuex;
                if (bilix < minValuex)
                    bilix = maxValuex;
                if (valuey > maxValuey)
                    valuey = minValuey;
                if (valuey < minValuey)
                    valuey = maxValuey;
                mat.mainTextureOffset = new Vector2(valuex, valuey);
            }
        }
        #endregion
    }
}
