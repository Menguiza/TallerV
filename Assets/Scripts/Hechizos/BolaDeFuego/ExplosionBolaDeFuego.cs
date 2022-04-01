using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBolaDeFuego : MonoBehaviour
{
    public int damage;

    float deltaAlpha = 0.4f;

    Material material;

    private void Awake()
    {
        material = gameObject.GetComponent<MeshRenderer>().material;
    }

    private void Update()
    {
        //Perder alpha progresivamente
        if (material.color.a <= 0)
        {
            Destroy(gameObject);
        }

        material.color = new Color(material.color.r, material.color.g, material.color.b, material.color.a - (deltaAlpha * Time.deltaTime));

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<EnemyController>() != null)
        {
            other.gameObject.GetComponent<EnemyController>().Life -= (uint)damage;
        }
    }
}
