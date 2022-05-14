using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChangeMusicManager : MonoBehaviour
{
    [SerializeField] GameObject awaken, dreamer;
    AudioSource awakenSC, dreamerSC;
    [SerializeField] float volumeSpeed, timeSlide;
    bool callAwake = false, callDreamer = false;

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
}
