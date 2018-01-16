/*************************************************************
   Copyright(C) 2017 by dayugame
   All rights reserved.
   
   DancerAni.cs
   PartyRhythmGame
   
   Created by WuIslet on 2018-01-15.
   
*************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace isletspace
{
    /// <summary>
    /// 
    /// </summary>
    public class DancerAni : MonoBehaviour
    {
        private Animator animator;

        /*
        private void OnGUI()
        {
            if (GUILayout.Button("pose1"))
            {
                DoPose(1);
            }
            if (GUILayout.Button("pose2"))
            {
                DoPose(2);
            }
            if (GUILayout.Button("pose3"))
            {
                DoPose(3);
            }
            if (GUILayout.Button("pose4"))
            {
                DoPose(4);
            }
            if (GUILayout.Button("action1"))
            {
                DoAction(1);
            }
            if (GUILayout.Button("action2"))
            {
                DoAction(2);
            }
            if (GUILayout.Button("action3"))
            {
                DoAction(3);
            }
            if (GUILayout.Button("cheer"))
            {
                DoCheer(true);
            }
            if (GUILayout.Button("cheerEnd"))
            {
                DoCheer(false);
            }
        }*/
        
        private void Start()
        {
            animator = gameObject.GetComponent<Animator>();
        }

        public void DoPose(int type)
        {
            animator.SetTrigger("pose");
            animator.SetFloat("poseType", type);
        }

        public void DoAction(int type)
        {
            animator.SetTrigger("drum");
            animator.SetFloat("drumType", type);
        }

        public void DoCheer(bool flag)
        {
            animator.SetBool("cheerFlag", flag);
            int type = Random.Range(1, 4);
            print("   do cheer  " + type);
            animator.SetFloat("cheerType", type);
        }

        public void DoAddLight(int type)
        {
            Material[] oriMat = null;
            MeshRenderer smr = GameObject.Find("gu").GetComponent<MeshRenderer>();
            //SkinnedMeshRenderer smr = GameObject.Find("gu").GetComponent<SkinnedMeshRenderer>();
            oriMat = smr.sharedMaterials;

            var mat1 = Resources.Load("prefab/bd_119") as Material;
            Material[] newMat = { oriMat[0], mat1 };
            smr.sharedMaterials = newMat;
        }

        public void DoDelLight(int type)
        {
            MeshRenderer smr = GameObject.Find("gu").GetComponent<MeshRenderer>();
            //SkinnedMeshRenderer smr = GameObject.Find("gu").GetComponent<SkinnedMeshRenderer>();
            Material[] oriMat = smr.sharedMaterials;

            Material[] newMat = { oriMat[0] };
            smr.sharedMaterials = newMat;
        }
    }
}
