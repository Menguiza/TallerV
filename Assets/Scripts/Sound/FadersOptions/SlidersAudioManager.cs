using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SlidersAudioManager : MonoBehaviour
{
    [Header("Chanels")]
    [SerializeField] AudioMixer mixer;

    public void SetSFXVolume(float sfxVolume)
    {
        mixer.SetFloat("sfxVolume", sfxVolume);
    }

    public void SetMusicVolume(float musicVolume)
    {
        mixer.SetFloat("musicVolume", musicVolume);
    }

    public void SetMixerVolume(float mixerVolume)
    {
        mixer.SetFloat("mixerVolume", mixerVolume);
    }
}
