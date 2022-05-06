using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil_BolaDeAcido : MonoBehaviour
{
    public float damage;

    [SerializeField] GameObject poissonExplotion;

    public void acidBallImpact()
    {
        //Aqui van las particulas --
        GameObject explotion = Instantiate(poissonExplotion, transform.localPosition, Quaternion.identity);
        Destroy(explotion, 4f);

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

        acidBallImpact();
    }
}
