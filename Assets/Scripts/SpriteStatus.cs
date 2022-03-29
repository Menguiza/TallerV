using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteStatus : MonoBehaviour
{
    GameMaster gm;
    float result;
    byte two = 2, one = 1, zero = 0;

    [SerializeField]
    Sprite amoSano, amoHerido, dormido, dormidoHerido, muerto;

    private void Awake()
    {
        gm = FindObjectOfType<GameMaster>();
    }

    void Update()
    {
        result = Mathf.Max(0, ((float)gm.Player.Life / (float)gm.Player.MaxLife));

        if (result > (float)one / two && result > zero && gm.Player.Status == GameMaster.estado.Despierto)
        {
            gameObject.GetComponent<Image>().sprite = amoSano;
        }
        else if(result > (float)one / two && result > zero && gm.Player.Status == GameMaster.estado.Dormido)
        {
            gameObject.GetComponent<Image>().sprite = dormido;
        }
        else if(result <= (float)one / two && result > zero && gm.Player.Status == GameMaster.estado.Despierto)
        {
            gameObject.GetComponent<Image>().sprite = amoHerido;
        }
        else if(result <= (float)one / two && result > zero && gm.Player.Status == GameMaster.estado.Dormido)
        {
            gameObject.GetComponent<Image>().sprite = dormidoHerido;
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = muerto;
        }
    }
}
