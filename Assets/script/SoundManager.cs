using UnityEngine;
using System.Collections;

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
    public void PlayMenuBackground(){
		AudioClip ac = Backgrounds [0];
        if(bgSource.isPlaying == false || bgSource.clip != ac)
        {
            bgSource.clip = ac;
            bgSource.Play();
        }
	}

	public void PlayShopBackground(){
		AudioClip ac = Backgrounds [1];
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
    #endregion

    public void PlayLaZha()
    {
        aSource.PlayOneShot(EffectSounds[0]);
    }

    public void PlayFoot()
    {
        aSource.PlayOneShot(EffectSounds[1]);
    }

    public void PlayRoundStartSound()
    {
        aSource.PlayOneShot(EffectSounds[4]);
    }

    public void PlayShuffle()
    {
        aSource.PlayOneShot(EffectSounds[5]);
    }

    public void PlayDizeSound()
    {
        aSource.PlayOneShot(EffectSounds[6]);
    }

    public void PlayGet4Card()
    {
        aSource.PlayOneShot(EffectSounds[7]);
    }

    public void PlayDropCard()
    {
        aSource.PlayOneShot(EffectSounds[8]);
    }

    public void PlaySelectCard()
    {
        aSource.PlayOneShot(EffectSounds[9]);
    }

    public void PlayRoundEndSound()
    {
        aSource.PlayOneShot(EffectSounds[11]);
    }

    public void PlaySettleSound()
    {
        aSource.PlayOneShot(EffectSounds[12]);
    }

    public void PlayHuSound()
    {
        aSource.PlayOneShot(EffectSounds[13]);
    }

    public void PlayTingSound()
    {
        aSource.PlayOneShot(EffectSounds[15]);
    }

    public void PlayGetCard()
    {
        aSource.PlayOneShot(EffectSounds[16]);
    }

    public void PlayFall()
    {
        aSource.PlayOneShot(EffectSounds[17]);
    }

    public void PlayBuy()
    {
        aSource.PlayOneShot(EffectSounds[18]);
    }

    public void PlayLose()
    {
        aSource.PlayOneShot(EffectSounds[19]);
    }

    public void PlayWin()
    {
        aSource.PlayOneShot(EffectSounds[20]);
    }

    public void PlayEffect(int index)
    {
        aSource.PlayOneShot(EffectSounds[index]);
    }
}
