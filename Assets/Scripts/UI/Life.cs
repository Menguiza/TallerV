using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Life : MonoBehaviour
{
    GameMaster gm;
    float result;
    public TMP_Text text;
    public TMP_Text units;
    byte porcentual = 100;

    private void Awake()
    {
        gm = FindObjectOfType<GameMaster>();
    }

    void Update()
    {
        if (gm.playerObject != null)
        {
            result = Mathf.Max(0, ((float)gm.Player.Life / (float)gm.Player.MaxLife));
            gameObject.GetComponent<Image>().fillAmount = result;

            if (text != null && units != null)
            {
                text.text = (Math.Round(result, 2) * porcentual + "%");

                units.text = gm.Player.Life.ToString();
            }
        }

    }
}
