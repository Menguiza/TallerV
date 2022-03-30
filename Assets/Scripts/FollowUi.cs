using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class FollowUi : MonoBehaviour
{
    [SerializeField]
    GameObject worldPos; // Objeto a Seguir
    [SerializeField]
    RectTransform rectTrans; // UI elements
    public Vector2 offset; // Offset

    float distance; //Distancia con respecto al jugador

    void Update()
    {
        if(worldPos != null)
        {
            Vector2 screenPos = Camera.main.WorldToScreenPoint(worldPos.transform.position);
            rectTrans.position = screenPos + offset;

            distance = Vector3.Distance(FindObjectOfType<PlayerController>().transform.position, worldPos.transform.position);
        }
        else if(worldPos == null)
        {
            Destroy(gameObject);
        }
    }
}
