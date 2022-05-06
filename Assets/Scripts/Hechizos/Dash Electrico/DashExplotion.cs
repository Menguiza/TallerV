using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashExplotion : MonoBehaviour
{
    public float damage;

    [SerializeField] GameObject impactExplotion;

    private void Awake()
    {
        Destroy(gameObject, 0.5f);

        impactExplotion.transform.SetParent(GameMaster.instance.gameObject.transform);
        Destroy(impactExplotion, 4f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<IEnemy>() != null)
        {
            other.gameObject.GetComponent<IEnemy>().ReceiveDamage(GameMaster.instance.CalculateSpellDamage(damage));
        }
        else if (other.gameObject.GetComponent<EnemyController>() != null)
        {
            other.gameObject.GetComponent<EnemyController>().ReceiveDamage(GameMaster.instance.CalculateSpellDamage(damage));
        }
    }
}
