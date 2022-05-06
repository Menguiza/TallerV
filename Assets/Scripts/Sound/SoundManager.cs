using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource audioSource;

    [Header("Amo - sounds")]
    [SerializeField] AudioClip amoSteps;
    [SerializeField] AudioClip jumpHop;
    [SerializeField] AudioClip jumpLand;
    [SerializeField] AudioClip roll;
    [SerializeField] AudioClip swordSwing;
    [SerializeField] AudioClip death;



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

    #region"Spells - SFX"
    #endregion

    #region"Spells - Cast sounds"
    #endregion

    #region"Fairy"
    #endregion

    #region"Enemy"
    #endregion

    #endregion
}
