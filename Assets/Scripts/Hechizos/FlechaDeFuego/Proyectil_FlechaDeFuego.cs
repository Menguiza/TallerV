using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil_FlechaDeFuego : MonoBehaviour
{
    public int damage;

    public void fireArrowImpact()
    {
        //Aqui van las particulas --

        //Separar el trail
        Destroy(gameObject.transform.GetChild(0).gameObject, 1f);

        gameObject.transform.DetachChildren();

        //Siempre Eliminar al final el proyectil
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<EnemyController>() != null)
        {
            other.gameObject.GetComponent<EnemyController>().Life -= (uint)damage;
        }

        fireArrowImpact();
    }
}
