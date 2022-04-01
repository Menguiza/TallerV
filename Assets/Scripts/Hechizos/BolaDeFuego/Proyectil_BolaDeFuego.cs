using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil_BolaDeFuego : MonoBehaviour
{
    public int damage;
    [SerializeField] GameObject FireballExplosionVolume;

    public void fireballExplode()
    {
        //Zona de explosión
        GameObject explosion = Instantiate(FireballExplosionVolume, transform.position, Quaternion.identity);
        explosion.GetComponent<ExplosionBolaDeFuego>().damage = damage;

        //Aqui van las particulas --

        //Separar el trail

        //Siempre Eliminar al final el proyectil
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        fireballExplode();
    }
}
