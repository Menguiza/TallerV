using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToGameStart : MonoBehaviour
{
    void ChangeScene()
    {
        SceneManager.LoadScene(3);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ChangeScene();
        }
    }
}
