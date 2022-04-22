using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBolaDeFuego : MonoBehaviour
{
    public float damage;

    private void Awake()
    {
        Destroy(gameObject, 2);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<EnemyController>() != null)
        {
            other.gameObject.GetComponent<EnemyController>().ReceiveDamage(GameMaster.instance.CalculateSpellDamage(damage));
        }
    }
}
