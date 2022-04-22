using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMusicManager : MonoBehaviour
{
    [SerializeField] GameObject awaken, dreamer;
    AudioSource awakenSC, dreamerSC;
    [SerializeField] float volumeSpeed, timeSlide;
    bool callAwake = false, callDreamer = false;

    void Awake()
    {
        awaken = transform.GetChild(0).gameObject;
        awakenSC = awaken.GetComponent<AudioSource>();
        dreamer = transform.GetChild(1).gameObject;
        dreamerSC = dreamer.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !callAwake && awakenSC.volume > 0)
        {
            callAwake = true;
            StartCoroutine(AwakenManager());
        }  
        
        if (Input.GetKeyDown(KeyCode.G) && !callDreamer && dreamerSC.volume > 0)
        {
            callDreamer = true;
            StartCoroutine(DreamerManager());
        }
    }

    IEnumerator AwakenManager()
    {
        while (awakenSC.volume > 0)
        {
            awakenSC.volume -= volumeSpeed;
            dreamerSC.volume += volumeSpeed;
            yield return new WaitForSeconds(timeSlide);
        }
        callAwake = false;
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
        callDreamer = false;
        yield return null;
    }
}
