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

        private Material[] oldMaterial;

        #region 常数
        Vector3 target = new Vector3(-500, 0, 0);
        #endregion
        
        private void Start()
        {
            animator = gameObject.GetComponent<Animator>();
            mid = (transform.position + target) / 2;
            lightEffectPos = transform.Find("LightEffect");
        }

        public void DoPose()
        {
            animator.speed = 1;
            animator.SetTrigger("pose");
            int type = Random.Range(1, 7);
            animator.SetFloat("poseType", type);
        }

        public void DoDrum(int type, float time)
        {
            float rate = 1;
            if (time != 0 && time < 0.5f)
            {
                rate = 0.5f / time;
            }
            animator.speed = rate;
            

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
            animator.speed = 1;
            animator.SetBool("cheerFlag", flag);
            if (flag)
            {
                int type = Random.Range(1, 4);
                animator.SetFloat("cheerType", type);
            }
        }

        #region 光点飞行动画
        public void DoLightSpotMove(float startTime = 0, System.Action callback = null)
        {
            StartCoroutine(MoveLightSpot(20, 3f, startTime, callback));
        }

        private IEnumerator MoveLightSpot(int d, float time, float startTime, System.Action callback)
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
            
            if(callback != null)
            {
                callback();
            }

            Destroy(obj);
        }
        #endregion

        #region 片头动作
        public void PlayOP()
        {
            animator.SetTrigger("OP");
        }
        public void PlayOPPose()
        {
            animator.SetTrigger("OPpose");
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
            animator.speed = 1;
            animator.SetBool("EndLeading", flag);
        }
        #endregion

        #region 击鼓特效
        public void DoAddLight()
        {/*
            //MeshRenderer smr = Utils.FindDirectChildComponent<MeshRenderer>("gu", transform);
            SkinnedMeshRenderer smr = Utils.FindDirectChildComponent<SkinnedMeshRenderer>("gu", transform);
            oldMaterial = smr.materials;

            var mat1 = Resources.Load("prefab/bd_119") as Material;
            Material[] newMat = { mat1 };
            smr.materials = newMat;*/
            var ani = Utils.FindDirectChildComponent<Animator>("gu", transform);
            ani.SetTrigger("doflash");
        }

        public void DoDelLight()
        {
            /*
            //MeshRenderer smr = Utils.FindDirectChildComponent<MeshRenderer>("gu", transform);
            SkinnedMeshRenderer smr = Utils.FindDirectChildComponent<SkinnedMeshRenderer>("gu", transform);
            smr.materials = oldMaterial;
            */
        }
        #endregion

        #region 自动序列击鼓
        public IEnumerator DrumRandom(int count, float totalTime)
        {
            for (int i = 0; i < count; ++i)
            {
                float gap = Random.Range(0.5f, 1f);
                int type = Random.Range(1, 4);
                if (Random.Range(0f, 1f) < 0.3f)
                {
                    DoDrum(type, gap);
                }
                yield return new WaitForSeconds(gap);
                totalTime -= gap;
                if(totalTime < 0)
                {
                    yield break;
                }
            }
        }
        public IEnumerator DrumConstant(int type, int count, float gap)
        {
            for (int i = 0; i < count; ++i)
            {
                DoDrum(type, gap);
                yield return new WaitForSeconds(gap);
            }
        }

        public IEnumerator DrumList(List<int> beat, List<int> beattime, System.Action<int> callback = null)
        {
            int preTime = 0;
            for (int i = 0; i < beat.Count; ++i)
            {
                float gap = beattime[i] / 1000.0f;
                yield return new WaitForSeconds(gap - 0.3f);
                DoDrum(beat[i], gap);
                yield return new WaitForSeconds(0.3f);
                if(callback != null)
                {
                    callback(beat[i]);
                }
                preTime = beattime[i];
            }
        }
        #endregion
    }
}
