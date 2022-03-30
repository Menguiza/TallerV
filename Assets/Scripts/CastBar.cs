using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastBar : MonoBehaviour
{
    [SerializeField]
    CanvasGroup cnvGroup;
    GameMaster gm;
    [SerializeField]
    Image fill;
    [SerializeField]
    AnimationClip clip;
    bool called = false;

    private void Start()
    {
        gm = FindObjectOfType<GameMaster>();
    }

    private void Update()
    {
        if (gm.playerObject != null)
        {
            if (gm.playerObject.GetComponent<PlayerController>().attack && !called)
            {
                called = true;
                fill.fillAmount = 0;
                cnvGroup.alpha = 1;
            }
            else if (!gm.playerObject.GetComponent<PlayerController>().attack)
            {
                cnvGroup.alpha = 0;
            }
        }
    }

    private void FixedUpdate()
    {
        if(gm.playerObject != null)
        {
            FillBar();
        }
    }

    void FillBar()
    {
        if (gm.playerObject.GetComponent<PlayerController>().attack)
        {
            fill.fillAmount += 1 / ((clip.length / gm.Player.MultVelAtaque) * 50);
        }
        else if (!gm.playerObject.GetComponent<PlayerController>().attack)
        {
            called = false;
        }
    }
}
