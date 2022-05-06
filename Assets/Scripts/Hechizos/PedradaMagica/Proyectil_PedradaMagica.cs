using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil_PedradaMagica : MonoBehaviour
{
    public float damage;

    [SerializeField] GameObject vfx_pebble;

    public void magicPebbleImpact()
    {
        //Aqui van las particulas --
        vfx_pebble.transform.SetParent(null);
        vfx_pebble.GetComponent<ParticleSystem>().Play();
        Destroy(vfx_pebble, 3f);

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

        magicPebbleImpact();
    }
}
