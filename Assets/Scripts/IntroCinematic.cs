using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroCinematic : MonoBehaviour
{
    private void Start()
    {
        Invoke(nameof(ToMainMenu), 4f);
    }

    void ToMainMenu()
    {
        SceneManager.LoadScene(1);
    }
}
