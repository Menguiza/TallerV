using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ChangeMusicManager : MonoBehaviour
{
    [SerializeField] GameObject awaken, dreamer;
    AudioSource awakenSC, dreamerSC;
    [SerializeField] float volumeSpeed, timeSlide;
    bool callAwake = false, callDreamer = false;

    [SerializeField] bool markedAsBossSoundManager;

    void Start()
    {
        awaken = transform.GetChild(0).gameObject;
        awakenSC = awaken.GetComponent<AudioSource>();
        dreamer = transform.GetChild(1).gameObject;
        dreamerSC = dreamer.GetComponent<AudioSource>();

        GameMaster.instance.PlayerDream.AddListener(AwakenVolume);
        GameMaster.instance.PlayerWake.AddListener(DreamerVolume);
    }

    //!callAwake && awakenSC.volume > 0
    //!callDreamer && dreamerSC.volume > 0

    IEnumerator AwakenManager()
    {
        if (SceneManager.GetActiveScene().buildIndex == 9 && !markedAsBossSoundManager)
        {
            yield break;
        }

        while (awakenSC.volume > 0)
        {
            awakenSC.volume -= volumeSpeed;
            dreamerSC.volume += volumeSpeed;
            yield return new WaitForSeconds(timeSlide);
        }
        yield return null;
    }

    IEnumerator DreamerManager()
    {
        if (SceneManager.GetActiveScene().buildIndex == 9 && !markedAsBossSoundManager)
        {
            yield break;
        }

        while (dreamerSC.volume > 0)
        {
            awakenSC.volume += volumeSpeed;
            dreamerSC.volume -= volumeSpeed;
            yield return new WaitForSeconds(timeSlide);
        }
        yield return null;
    }

    void DreamerVolume()
    {
            StopAllCoroutines();
            StartCoroutine(DreamerManager());
        
    }

    void AwakenVolume()
    {
            StopAllCoroutines();
            StartCoroutine(AwakenManager());
    }

    private void OnDisable()
    {
        if (markedAsBossSoundManager)
        {
            GameMaster.instance.PlayerDream.RemoveListener(AwakenVolume);
            GameMaster.instance.PlayerWake.RemoveListener(DreamerVolume);
        }      
    }
}
