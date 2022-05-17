using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource audioSource;

    [SerializeField] AudioMixerSnapshot paused, unpaused;



    void Awake()
    {
        #region "Singleton"
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        #endregion

        audioSource = GetComponent<AudioSource>();
    }

    #region"Sound methods"

    public void PlayClip(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }

    #region "Amo - sounds"
    public void PlayAmoStep()
    {

    }
    #endregion

    public void SetPauseMusic()
    {
        paused.TransitionTo(0);
    }

    public void SetUnpausedMusic()
    {
        unpaused.TransitionTo(0);
    }
    #endregion
}
