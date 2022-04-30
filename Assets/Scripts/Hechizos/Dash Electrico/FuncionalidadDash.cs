using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuncionalidadDash : MonoBehaviour
{
    public float damage;

    float timeTillStop = 0.1f;
    float timeSinceStart = 0f;

    GameObject explotion;

    private void Awake()
    {
        explotion = (GameObject)Resources.Load("Prefabs/Hechizos/LightningArrivalExplotion");
    }

    private void FixedUpdate()
    {
        // Recorrer 4 metros en un tiempo de 0.1 segundos (Se realiza en 5 frames)
        if (timeSinceStart < timeTillStop) transform.Translate(Vector3.forward * 40 * Time.fixedDeltaTime);
        else
        {
            GameObject exp = Instantiate(explotion, transform.position, Quaternion.identity);
            exp.GetComponent<DashExplotion>().damage = damage;
            Destroy(this);
        }

        timeSinceStart += Time.fixedDeltaTime;
    }
}
