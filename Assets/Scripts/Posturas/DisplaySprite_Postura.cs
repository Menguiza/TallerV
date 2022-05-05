using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplaySprite_Postura : MonoBehaviour
{
    Sprite stanceSprite;

    private void Start()
    {
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
        stanceSprite = GameMaster.instance.posturaDelSueño.icon;
    }

    public void SetStanceSprite()
    {
        GetComponent<Image>().sprite = stanceSprite;
    }
}
