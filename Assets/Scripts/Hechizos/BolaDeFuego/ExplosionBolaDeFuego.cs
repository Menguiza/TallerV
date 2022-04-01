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
        Destroy(gameObject, 2);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<EnemyController>() != null)
        {
            other.gameObject.GetComponent<EnemyController>().Life = (uint)Mathf.Max(0, other.gameObject.GetComponent<EnemyController>().Life - damage);
        }
    }
}
