using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage7 : MonoBehaviour
{
    [SerializeField] TutorialNPC npc;
    [SerializeField] GameObject enemy;

    public bool killedEnemy;
    bool flag;

    void Update()
    {
        if (enemy == null)
        {
            killedEnemy = true;
        }

        if (killedEnemy && !flag)
        {
            npc.textNPC.text = "Por �ltimo, usa 'Escape' para abrir la pausa si en alg�n momento necesitas respirar, ajustar o ver los controles (Tambi�n lo puedes hacer con F1), espero que nos puedas salvar!";

            flag = true;
        }
    }
}
