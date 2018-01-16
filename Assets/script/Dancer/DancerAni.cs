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
using DG.Tweening;

namespace isletspace
{
    /// <summary>
    /// 
    /// </summary>
    public class DancerAni : MonoBehaviour
    {
        private Animator animator;
        private Vector3 mid;
        private Transform lightEffectPos;
        public GameObject SpotLight;

        #region ³£Êý
        Vector3 target = new Vector3(-500, 0, 0);
        #endregion

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
            if (GUILayout.Button("lightfly"))
            {
                DoLightMove();
            }
        }
        */
        
        private void Start()
        {
            animator = gameObject.GetComponent<Animator>();
            mid = (transform.position + target) / 2;
            lightEffectPos = transform.Find("LightEffect");
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
            if (flag)
            {
                int type = Random.Range(1, 4);
                print("   do cheer  " + type);
                animator.SetFloat("cheerType", type);
            }
        }

        public void DoLightMove()
        {
            StartCoroutine(MoveLight(20, 5f));
        }

        private IEnumerator MoveLight(int d, float time)
        {
            var obj = Pool.CreateObject("lightspot/lightspot", lightEffectPos);

            //move obj
            mid.y = Random.Range(5.0f, 10.0f);
            float timegap = time / d;
            for (int i = 0; i < d; ++i)
            {
                Vector3 point = BezierGenerate.CreatePoint(obj.transform.position, mid, target, (i + 1f) / d);
                obj.transform.DOMove(point, timegap).SetEase(Ease.Linear);
                yield return new WaitForSeconds(timegap);
            }
            
            Destroy(obj);
        }

        public void PlayOP()
        {
            SpotLight.SetActive(true);
            animator.SetTrigger("OP");
        }

        public void PlayEndOP()
        {
            SpotLight.SetActive(false);
        }

        public void PlayJoin()
        {
            animator.SetTrigger("startjoin");
        }
        public void PlayEndJoin()
        {
            animator.SetTrigger("endjoin");
        }
    }
}
