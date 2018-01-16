/*************************************************************
   Copyright(C) 2017 by dayugame
   All rights reserved.
   
   PlayMovie.cs
   PartyRhythmGame
   
   Created by WuIslet on 2018-01-10.
   
*************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace isletspace
{
    /// <summary>
    /// 
    /// </summary>
    public class PlayMovie : MonoBehaviour
    {
        public MovieTexture movieTexture;
        public AudioSource audioSource;

        private void Awake()
        {
            movieTexture.loop = true;
            audioSource.clip = movieTexture.audioClip;
            var image = gameObject.GetComponent<Image>();
            image.material.mainTexture = movieTexture;
        }

        public void DoPlay()
        {
            movieTexture.Play();
            audioSource.Play();
        }

        public void DoPause()
        {
            movieTexture.Pause();
            audioSource.Pause();
        }

        public void DoStop()
        {
            movieTexture.Stop();
            audioSource.Stop();
        }
    }
}
