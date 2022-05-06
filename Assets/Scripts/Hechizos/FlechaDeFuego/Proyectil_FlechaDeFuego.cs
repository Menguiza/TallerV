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

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.GetComponent<IEnemy>() != null)
        {
            collider.gameObject.GetComponent<IEnemy>().ReceiveDamage(GameMaster.instance.CalculateSpellDamage(damage));
        }
        else if (collider.gameObject.GetComponent<EnemyController>() != null)
        {
            collider.gameObject.GetComponent<EnemyController>().ReceiveDamage(GameMaster.instance.CalculateSpellDamage(damage));
        }

        fireArrowImpact();
    }
}
