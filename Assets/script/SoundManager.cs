using UnityEngine;
using System.Collections;
using DG.Tweening;

public class SoundManager : ISingleton<SoundManager> {
	public AudioClip[] Backgrounds;
    public AudioClip[] EffectSounds;

    public AudioSource bgSource;
    public AudioSource aSource;

    public void MuteSound(bool mute)
    {
        aSource.mute = !mute;
    }

    public void MuteMusic(bool mute)
    {
        bgSource.mute = !mute;
    }

    #region BGM
    public void PlaySoloBackground(){
		AudioClip ac = Backgrounds [3];
        if(bgSource.isPlaying == false || bgSource.clip != ac)
        {
            bgSource.clip = ac;
            bgSource.Play();
        }
    }

    public void PlayGuideBackground()
    {
        AudioClip ac = Backgrounds[4];
        if (bgSource.isPlaying == false || bgSource.clip != ac)
        {
            bgSource.clip = ac;
            bgSource.Play();
        }
    }

    public void PlayJoinBackground()
    {
        AudioClip ac = Backgrounds[5];
        if (bgSource.isPlaying == false || bgSource.clip != ac)
        {
            bgSource.clip = ac;
            bgSource.Play();
        }
    }

    public void PlayBackground(int no)
    {
        AudioClip ac = Backgrounds[no];
        if (bgSource.isPlaying == false || bgSource.clip != ac)
        {
            bgSource.clip = ac;
            bgSource.Play();
        }
    }
    
    public void StopBackground()
    {
        if (bgSource.isPlaying)
        {
            bgSource.Stop();
        }
    }

    public void FadeBackground(float time)
    {
        StartCoroutine(DelayFadeBackground(time));
    }

    private IEnumerator DelayFadeBackground(float time)
    {
        if (!bgSource.isPlaying)
        {
            yield break;
        }
        
        if (time > 3)
        {
            yield return new WaitForSeconds(time - 3);
            time = 3;
        }

        bgSource.DOFade(0, time - 0.5f).onComplete = ()=> {
            bgSource.volume = 1;
            bgSource.Stop();
        };
    }
    #endregion

    public void PlayLaZha()
    {
        aSource.PlayOneShot(EffectSounds[0]);
    }

    public void PlayFoot()
    {
        aSource.PlayOneShot(EffectSounds[1]);
    }

    public void PlaySingleA()
    {
        aSource.PlayOneShot(EffectSounds[2]);
    }

    public void PlaySingleB()
    {
        aSource.PlayOneShot(EffectSounds[3]);
    }

    public void PlaySingleAB()
    {
        aSource.PlayOneShot(EffectSounds[4]);
    }

    public void PlayAllA()
    {
        aSource.PlayOneShot(EffectSounds[5]);
    }

    public void PlayAllB()
    {
        aSource.PlayOneShot(EffectSounds[6]);
    }

    public void PlayAllAB()
    {
        aSource.PlayOneShot(EffectSounds[7]);
    }

    public void PlayCollect()
    {
        aSource.PlayOneShot(EffectSounds[8]);
    }

    public void PlayCheerFX()
    {
        aSource.PlayOneShot(EffectSounds[9]);
    }

    public void PlayGO()
    {
        aSource.PlayOneShot(EffectSounds[10]);
    }

    public void PlayEffect(int index)
    {
        aSource.PlayOneShot(EffectSounds[index]);
    }
}
