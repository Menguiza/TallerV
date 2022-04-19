using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Daño : MonoBehaviour
{
    GameMaster gm;
    public TMP_Text text;

    private void Awake()
    {
        gm = FindObjectOfType<GameMaster>();
    }
    void Update()
    {
        text.text = "Damage: " + gm.Player.Damage;
    }
}
