using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil_BolaDeAcido : MonoBehaviour
{
    public float damage;

    public void acidBallImpact()
    {
        //Aqui van las particulas --

        //Siempre Eliminar al final el proyectil
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.GetComponent<EnemyController>() != null)
        {
            collider.gameObject.GetComponent<EnemyController>().ReceiveDamage(GameMaster.instance.CalculateSpellDamage(damage));
        }

        acidBallImpact();
    }
}
