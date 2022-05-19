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
            npc.textNPC.text = "Por último, usa 'Escape' para abrir la pausa si en algún momento necesitas respirar, ajustar o ver los controles (También lo puedes hacer con F1), espero que nos puedas salvar!";

            flag = true;
        }
    }
}
