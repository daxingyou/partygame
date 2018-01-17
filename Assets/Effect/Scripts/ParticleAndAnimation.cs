using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

[Serializable]
public class AnimatorData
{
	public Animator ani;
	public string aniName;
    public string stateName;
	public float second;
    public float speed;

    public AnimatorData()
    {
        speed = 1.0f;
    }
}


public class ParticleAndAnimation : MonoBehaviour
{
	public string ChangeAni;
	public string ChangeStopAni;
	public lfAniController aniController;

    public float Scale = 1.0f;
	public AnimatorData[] mAnimators;

    public AnimatorData[] mAnimators2;

    public ParticleSystem[] pss;
    public List<Animation> anis;
    public List<ParticleAndAnimation> paas;

    public bool allParticleSystem = false;
    public bool allAnimation = false;
    public bool allSub = false;

    public bool AutoPlay = false;
    [NonSerialized]
    public bool isPlaying = false;
    public float EffEndTime;//lihui 添加动画遮罩时间 2014/10/20

    public void Awake()
    {
        if (allParticleSystem)
        {
            pss = GetComponentsInChildren<ParticleSystem>(true);
        }
        

        if (allAnimation)
        {
            anis = new List<Animation>(GetComponentsInChildren<Animation>(true));
        }

        if (allSub)
        {
            paas = new List<ParticleAndAnimation>(GetComponentsInChildren<ParticleAndAnimation>(true));
            paas.Remove(this);
        }
        
        List<Animation> removeList=new List<Animation>();
        foreach (Animation ani in anis)
        {
            foreach (AnimatorData animatorData in mAnimators)
            {
                if (animatorData.ani == ani)
                {
                    removeList.Add(ani);
                    break;
                }
            }
        }

        foreach (Animation ani in anis)
        {
            foreach (ParticleAndAnimation paa in paas)
            {
                foreach (AnimatorData animatorData in paa.mAnimators)
                {
                    if (animatorData.ani == ani)
                    {
                        removeList.Add(ani);
                        break;
                    }
                }
            }
        }

        while (removeList.Count>0)
        {
            anis.Remove(removeList[0]);
            removeList.RemoveAt(0);
        }

        for (int i = 0; i < pss.Length; i++)
        {
            pss[i].startSize *= Scale;
            pss[i].playOnAwake = false;
        }
        this.transform.localScale=new Vector3(Scale,Scale,Scale);
    }

	void Start () 
	{
        if (AutoPlay)
		    PlayOnce();
	}

    [ContextMenu("Stop")]
    public void Stop()
    {
        isPlaying = false;
		//public string ChangeStopAni;
		//public lfAniController aniController;
		if(aniController!=null)
		{
			aniController.AniName = ChangeStopAni;
		}
        foreach (ParticleSystem ps in pss)
        {
            ps.Stop(false);
            ps.Clear(false);
        }
        foreach (Animation an in anis)
        {
            an.Stop();
        }
        for (int i = 0; i < mAnimators.Length; i++)
        {
            AnimatorData data = mAnimators[i];
            //aniData.ani.StopPlayback();
            if (!string.IsNullOrEmpty(data.stateName))
            {
                data.ani.SetBool(data.stateName, false);
            }
        }

        foreach (ParticleAndAnimation paa in paas)
        {
            paa.Stop();
        }
        this.gameObject.SetActive(false);
    }

    //lihui 暂停动画播放不隐藏 2014/10/20
    public void StopAnimitor(bool pause)
    {
        if (mAnimators2 != null)
        {
            foreach (var data in mAnimators2)
            {
                data.ani.speed = pause ? 0 : 1;
                if (!pause)
                    data.ani.Play(data.aniName, 0);
            }
        }
    }

    [ContextMenu("Play Loop")]
    public void PlayLoop()
    {
        if (isPlaying)
            return;

        isPlaying = true;
        this.gameObject.SetActive(true);
        StopAnimitor(false);
        foreach (ParticleSystem ps in pss)
        {
            ps.loop = true;
            ps.Play(false);
        }
        foreach (Animation an in anis)
        {
            an.wrapMode = WrapMode.Loop;
            an.Play();
        }

        for (int i = 0; i < mAnimators.Length; i++)
        {
            AnimatorData aniData = mAnimators[i];
            StartCoroutine(WaitAndPlay(aniData));
        }
        foreach (ParticleAndAnimation paa in paas)
        {
            paa.PlayLoop();
        }
    }

    IEnumerator WaitAndPlay(AnimatorData data)
    {
        yield return new WaitForSeconds(data.second);

        if (data.ani != null)
        {
            //data.ani.enabled=true;
            data.ani.speed = data.speed;
            if (!string.IsNullOrEmpty(data.aniName))
            {
                data.ani.Play(data.aniName, 0);
            }
            if (!string.IsNullOrEmpty(data.stateName))
            {
                data.ani.SetBool(data.stateName, true);
            }
        }
    }
	
	[ContextMenu("Play Once")]
	public void PlayOnce () 
	{
        this.gameObject.SetActive(true);
		if (aniController != null) {
			aniController.AniName = ChangeAni;		
		}
		foreach(ParticleSystem ps in pss)
		{
			ps.loop = false;
		    ps.Play(false);
		}
		foreach(Animation an in anis)
		{
            an.wrapMode = WrapMode.Once;
            an.Play();
        }
        StopAnimitor(false);
        for (int i = 0; i < mAnimators.Length; i++)
        {
            AnimatorData aniData = mAnimators[i];
            StartCoroutine(WaitAndPlay(aniData));
        }

	    foreach (ParticleAndAnimation paa in paas)
	    {
            if (paa!=null)
	            paa.PlayOnce();
	    }
	}
}
