using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public CanvasGroup cnvGrp;
    public Transform fadePoint;
    float distance; //Distancia con respecto al jugador

    [SerializeField]
    float minDis = 4f, minDisFade = 0.5f; //Distancia minima para mostar UI

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(FindObjectOfType<PlayerController>().transform.position, fadePoint.position);

        if (distance > minDis)
        {
            cnvGrp.alpha = 0;
        }
        else
        {
            if(distance>minDisFade)
            {
                cnvGrp.alpha = 1;
            }
            else
            {
                cnvGrp.alpha = 0.6f;
            }
        }
    }
}
