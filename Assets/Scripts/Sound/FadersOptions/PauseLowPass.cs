using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PauseLowPass : MonoBehaviour
{
    [SerializeField] AudioMixerSnapshot paused, unpaused;

    bool isPaused = false;
    private void Update()
    {
        if (!isPaused && Input.GetKeyDown(KeyCode.Escape)) 
        {
            print("a");
            paused.TransitionTo(0);
            isPaused = true;
        }

        else if (isPaused && Input.GetKeyDown(KeyCode.Escape))
        {
            print("b");
            unpaused.TransitionTo(0);
            isPaused = false;
        }
    }
}
