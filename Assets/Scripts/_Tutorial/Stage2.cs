using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2 : MonoBehaviour
{
    [SerializeField] EnergyBarrier barrier;
    [SerializeField] TutorialNPC npc;
    public bool tasDash;
    bool flag;

    void Update()
    {
        if (Input.GetButtonDown("Dodge"))
        {
            tasDash = true;
        }

        if (tasDash && !flag)
        {
            barrier.DestroyBarrier();

            npc.SetStage3();

            Destroy(gameObject, 6f);
            flag = true;
        }
    }
}
