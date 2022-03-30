using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplaySprite_Postura : MonoBehaviour
{
    GameMaster gm;
    Sprite stanceSprite;

    private void Start()
    {
        gm = FindObjectOfType<GameMaster>();
        GetStanceSprite();
    }

    //SOLO PARA LA ALPHA
    private void Update()
    {
        GetStanceSprite();
        SetStanceSprite();
    }

    void GetStanceSprite()
    {
        stanceSprite = gm.posturaDelSueño.icon;
    }

    public void SetStanceSprite()
    {
        GetComponent<Image>().sprite = stanceSprite;
    }
}
