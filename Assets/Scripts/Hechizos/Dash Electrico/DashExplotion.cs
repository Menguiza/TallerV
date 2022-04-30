using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashExplotion : MonoBehaviour
{
    public float damage;

    private void Awake()
    {
        Destroy(gameObject, 0.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<EnemyController>() != null)
        {
            other.gameObject.GetComponent<EnemyController>().ReceiveDamage(GameMaster.instance.CalculateSpellDamage(damage));
        }
    }
}
