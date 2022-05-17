using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyCount : MonoBehaviour
{
    [SerializeField]
    TMP_Text text;
    [SerializeField]
    GameObject continuar;
    bool llamado = false;

    // Update is called once per frame
    void Update()
    {
        text.text = FindObjectOfType<SceneChanger>().transform.childCount.ToString();

        if(int.Parse(text.text) == 0)
        {
            continuar.SetActive(true);

            if(!llamado)
            {
                EnemiesKilled();
            }
        }
    }

    void EnemiesKilled()
    {
        llamado = true;
        RoomManager.instance.onRoomFinished?.Invoke();
    }
}
