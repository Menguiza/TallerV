using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage3 : MonoBehaviour
{
    [SerializeField] EnergyBarrier barrier;
    [SerializeField] TutorialNPC npc;
    [SerializeField] GameObject crate;
    [SerializeField] Stage4 st4;
    public bool crateDestroy;
    bool flag;

    void Update()
    {
        if (crate == null)
        {
            crateDestroy = true;
        }

        if (crateDestroy && !flag)
        {
            npc.SetStage4();
            st4.enabled = true;

            flag = true;
        }
    }
}
