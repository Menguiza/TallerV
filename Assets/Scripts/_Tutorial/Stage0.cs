using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage0 : MonoBehaviour
{
    [SerializeField] EnergyBarrier barrier;
    [SerializeField] TutorialNPC npc;
    public bool taskInteract;
    bool flag;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            taskInteract = true;
        }

        if (taskInteract && !flag)
        {
            barrier.DestroyBarrier();

            npc.SetStage1();

            Destroy(gameObject, 6f);
            flag = true;
        }
    }
}
