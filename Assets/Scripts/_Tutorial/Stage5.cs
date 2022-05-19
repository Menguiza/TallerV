using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage5 : MonoBehaviour
{
    [SerializeField] EnergyBarrier barrier;
    [SerializeField] TutorialNPC npc;
    [SerializeField] GameObject crate;
    [SerializeField] Stage6 st6;

    public bool crateDestroyed;
    bool flag;

    void Update()
    {
        if (crate == null)
        {
            crateDestroyed = true;
        }

        if (crateDestroyed && !flag)
        {
            npc.SetStage6();
            st6.enabled = true;

            flag = true;
        }
    }
}
