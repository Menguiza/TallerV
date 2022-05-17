using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
        text.text = FindObjectOfType<SceneChanger>().transform.childCount.ToString();

        if(int.Parse(text.text) == 0)
        {
            me.GetComponentInParent<CanvasGroup>().alpha = 0;

            if(!llamado)
            {
                EnemiesKilled();
            }
        }
        else
        { 
            me.GetComponentInParent<CanvasGroup>().alpha = 1;
        }
    }

    void EnemiesKilled()
    {
        llamado = true;
        GameMaster.instance.OnRoomFinished?.Invoke();
        continuar.GetComponent<CanvasGroup>().alpha = 1;
    }
}
