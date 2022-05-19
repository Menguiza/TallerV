using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage6 : MonoBehaviour
{
    [SerializeField] EnergyBarrier barrier;
    [SerializeField] TutorialNPC npc;

    public bool hasTabed;
    bool flag;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            hasTabed = true;
        }

        if (hasTabed && !flag)
        {
            barrier.DestroyBarrier();

            npc.SetStage7();

            Destroy(gameObject, 6f);
            flag = true;
        }
    }
}
