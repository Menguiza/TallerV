using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Conciencia : MonoBehaviour
{
    GameMaster gm;
    float result;
    public TMP_Text text;
    public TMP_Text units;
    byte porcentual = 100;
    Image img;

    private void Awake()
    {
        gm = FindObjectOfType<GameMaster>();
        img = gameObject.GetComponent<Image>();
    }

    void Update()
    {
        result = Mathf.Max(0, ((float)gm.Player.Conciencia / (float)gm.Player.MaxConciencia));
        img.fillAmount = result;

        if(text != null && units != null)
        {
            text.text = (Math.Round(result, 2) * porcentual + "%");

            units.text = gm.Player.Conciencia.ToString();
        }
    }
}
