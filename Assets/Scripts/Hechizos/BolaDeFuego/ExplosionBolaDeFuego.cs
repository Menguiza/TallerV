using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBolaDeFuego : MonoBehaviour
{
    public float damage;

    [SerializeField] GameObject fireballExplotion;

    private void Awake()
    {
        Destroy(gameObject, 2);

        fireballExplotion.transform.SetParent(null);
        Destroy(fireballExplotion, 4f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<EnemyController>() != null)
        {
            other.gameObject.GetComponent<EnemyController>().ReceiveDamage(GameMaster.instance.CalculateSpellDamage(damage));
        }
    }
}
