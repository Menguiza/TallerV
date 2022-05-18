using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource audioSource;

    [SerializeField] AudioMixerSnapshot paused, unpaused;

    [SerializeField] AudioClip lightSwordSound;

    [SerializeField] AudioSource battleForestAwaken;
    [SerializeField] AudioSource battleForestDreamer;
    [SerializeField] AudioSource lobbyTheme;

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
        SetUnpausedMusic();
    }

    private void Start()
    {
        StartCoroutine(SubscribeToRunEnd());
    }

    #region"Sound methods"

    public void PlayClip(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }

    public void PlayLightSwordSound()
    {
        PlayClip(lightSwordSound);
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

    public void SetForestMusic()
    {
        battleForestAwaken.volume = 1;

        battleForestDreamer.volume = 0;
        lobbyTheme.volume = 0;
    }

    public void SetLobbyMusic()
    {
        lobbyTheme.volume = 1;

        battleForestAwaken.volume = 0;
        battleForestDreamer.volume = 0;
    }

    IEnumerator SubscribeToRunEnd()
    {
        yield return null;
        GameMaster.instance.OnRunEnd.AddListener(SetLobbyMusic);
    }
}
