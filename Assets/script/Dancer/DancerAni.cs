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
                DoPose();
            }
            if (GUILayout.Button("action1"))
            {
                DoDrum(1);
            }
            if (GUILayout.Button("action2"))
            {
                DoDrum(2);
            }
            if (GUILayout.Button("action3"))
            {
                DoDrum(3);
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
                DoLightSpotMove();
            }
            if (GUILayout.Button("drumlight"))
            {
                DoAddLight();
            }
            if (GUILayout.Button("drumend"))
            {
                DoDelLight();
            }
        }
        */
        
        private void Start()
        {
            animator = gameObject.GetComponent<Animator>();
            mid = (transform.position + target) / 2;
            lightEffectPos = transform.Find("LightEffect");
        }

        public void DoPose()
        {
            animator.SetTrigger("pose");
            int type = Random.Range(1, 7);
            animator.SetFloat("poseType", type);
        }

        public void DoDrum(int type)
        {
            animator.SetTrigger("drum");

            int realType = type;
            switch (type)
            {
                case 1:
                    realType = Random.Range(1, 3);
                    break;
                case 2:
                    realType = 3;
                    break;
                case 3:
                    realType = 4;
                    break;
            }
            animator.SetFloat("drumType", realType);
        }

        public void DoCheer(bool flag)
        {
            animator.SetBool("cheerFlag", flag);
            if (flag)
            {
                int type = Random.Range(1, 4);
                animator.SetFloat("cheerType", type);
            }
        }

        public void DoLightSpotMove(float startTime = 0)
        {
            StartCoroutine(MoveLightSpot(20, 3f, startTime));
        }

        private IEnumerator MoveLightSpot(int d, float time, float startTime)
        {
            if (startTime > 0)
            {
                yield return new WaitForSeconds(startTime);
            }

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
        public void DoYourTurn(bool flag)
        {
            animator.SetBool("EndLeading", flag);
        }

        public void DoAddLight()
        {
            Material[] oriMat = null;
            MeshRenderer smr = GameObject.Find("gu").GetComponent<MeshRenderer>();
            //SkinnedMeshRenderer smr = GameObject.Find("gu").GetComponent<SkinnedMeshRenderer>();
            oriMat = smr.sharedMaterials;

            var mat1 = Resources.Load("prefab/bd_119") as Material;
            Material[] newMat = { oriMat[0], mat1 };
            smr.sharedMaterials = newMat;
        }

        public void DoDelLight()
        {
            MeshRenderer smr = GameObject.Find("gu").GetComponent<MeshRenderer>();
            //SkinnedMeshRenderer smr = GameObject.Find("gu").GetComponent<SkinnedMeshRenderer>();
            Material[] oriMat = smr.sharedMaterials;

            Material[] newMat = { oriMat[0] };
            smr.sharedMaterials = newMat;
        }

        public IEnumerator RandomDrum(int count, float totalTime)
        {
            for (int i = 0; i < count; ++i)
            {
                float gap = Random.Range(0.5f, 3f);
                int type = Random.Range(1, 4);
                DoDrum(type);
                yield return new WaitForSeconds(gap);
                totalTime -= gap;
                if(totalTime < 0)
                {
                    yield break;
                }
            }
        }
    }
}
