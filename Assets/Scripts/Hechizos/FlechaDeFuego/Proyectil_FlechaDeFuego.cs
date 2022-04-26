using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil_FlechaDeFuego : MonoBehaviour
{
    public float damage;

    public void fireArrowImpact()
    {
        //Aqui van las particulas --

        //Siempre Eliminar al final el proyectil
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<EnemyController>() != null)
        {
            collision.gameObject.GetComponent<EnemyController>().ReceiveDamage(GameMaster.instance.CalculateSpellDamage(damage));
        }

        fireArrowImpact();
    }
}
