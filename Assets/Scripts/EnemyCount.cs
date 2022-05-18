using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EnemyCount : MonoBehaviour
{
    [SerializeField]
    TMP_Text text;
    [SerializeField]
    GameObject continuar, me;
    bool llamado = false;

    // Update is called once per frame
    void Update()
    {
        if(FindObjectOfType<SceneChanger>() != null)
        {
            text.text = FindObjectOfType<SceneChanger>().transform.childCount.ToString();
        }

        if(int.Parse(text.text) == 0)
        {
            me.GetComponent<CanvasGroup>().alpha = 0;

            if(!llamado)
            {
                EnemiesKilled();
            }
        }
        else
        { 
            me.GetComponent<CanvasGroup>().alpha = 1;
        }
    }

    void EnemiesKilled()
    {
        llamado = true;
        GameMaster.instance.OnRoomFinished?.Invoke();
        if(SceneManager.GetActiveScene().buildIndex != 4 && SceneManager.GetActiveScene().buildIndex != 2)
        {
            continuar.GetComponent<CanvasGroup>().alpha = 1;
        }
    }
}
