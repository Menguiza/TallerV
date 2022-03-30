using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExahustBar : MonoBehaviour
{
    [SerializeField]
    CanvasGroup cnvGroup;
    GameMaster gm;
    [SerializeField]
    Image fill;
    bool called = false;

    private void Start()
    {
        gm = FindObjectOfType<GameMaster>();
    }

    private void Update()
    {
        if (gm.playerObject != null)
        {
            if (!gm.playerObject.GetComponent<PlayerController>().dodgeEnable && !called)
            {
                called = true;
                fill.fillAmount = 0;
                cnvGroup.alpha = 1;
            }
            else if (gm.playerObject.GetComponent<PlayerController>().dodgeEnable)
            {
                cnvGroup.alpha = 0;
            }
        }
    }

    private void FixedUpdate()
    {
        FillBar();
    }

    void FillBar()
    {
        if (!gm.playerObject.GetComponent<PlayerController>().dodgeEnable)
        {
            fill.fillAmount += 1 / (gm.playerObject.GetComponent<PlayerController>().resetDodgeTime * 50);
        }
        else if (gm.playerObject.GetComponent<PlayerController>().dodgeEnable)
        {
            called = false;
        }
    }
}
