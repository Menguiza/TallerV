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

    [SerializeField]
    float minDis = 4; //Distancia minima para mostar UI

    void Update()
    {
        if(worldPos != null)
        {
            Vector2 screenPos = Camera.main.WorldToScreenPoint(worldPos.transform.position);
            rectTrans.position = screenPos + offset;

            distance = Vector3.Distance(FindObjectOfType<PlayerController>().transform.position, worldPos.transform.position);

            if (distance > minDis)
            {
                gameObject.GetComponent<Image>().enabled = false;
            }
            else
            {
                gameObject.GetComponent<Image>().enabled = true;
            }
        }
        else if(worldPos == null)
        {
            Destroy(gameObject);
        }
    }
}
