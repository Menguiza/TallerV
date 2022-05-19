using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage4 : MonoBehaviour
{
    [SerializeField] EnergyBarrier barrier;
    [SerializeField] TutorialNPC npc;

    public bool usedObj;
    bool flag;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            usedObj = true;
        }

        if (usedObj && !flag)
        {
            barrier.DestroyBarrier();

            npc.SetStage5();

            Destroy(gameObject, 6f);
            flag = true;
        }
    }
}
