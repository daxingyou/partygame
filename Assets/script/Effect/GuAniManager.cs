/*************************************************************
   Copyright(C) 2017 by dayugame
   All rights reserved.
   
   GuAniManager.cs
   PartyRhythmGame
   
   Created by WuIslet on 2018-01-22.
   
*************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace isletspace
{
    /// <summary>
    /// 
    /// </summary>
    public class GuAniManager : MonoBehaviour
    {
        Animator ani;
        GuLightTween gulight;
        SkinnedMeshRenderer smr;
        private Material[] oldMaterial;

        private void Start()
        {
            ani = gameObject.GetComponent<Animator>();
            gulight = transform.GetComponentInChildren<GuLightTween>();

            //MeshRenderer smr = Utils.FindDirectChildComponent<MeshRenderer>("gu", transform);
            smr = gameObject.GetComponent<SkinnedMeshRenderer>();
        }

        public void Begin()
        {
            /*
            oldMaterial = smr.materials;

            var mat1 = Resources.Load("prefab/bd_119") as Material;
            Material[] newMat = { mat1 };
            smr.materials = newMat;*/
            ani.SetTrigger("doflash");
            gulight.LightOnce(0.1f);
        }

        public void End()
        {
            /*
            smr.materials = oldMaterial;
            */
        }
    }
}
