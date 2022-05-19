using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1 : MonoBehaviour
{
    [SerializeField] EnergyBarrier barrier;
    [SerializeField] TutorialNPC npc;
    public bool taskMove;
    public bool taskJump;
    bool flag;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            taskMove = true;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            taskJump = true;
        }

        if (taskMove && taskJump && !flag)
        {
            barrier.DestroyBarrier();

            npc.SetStage2();

            Destroy(gameObject, 6f);
            flag = true;
        }
    }
}
