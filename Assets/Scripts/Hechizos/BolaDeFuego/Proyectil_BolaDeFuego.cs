using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil_BolaDeFuego : MonoBehaviour
{
    public float damage;
    [SerializeField] GameObject FireballExplosionVolume;

    public void fireballExplode()
    {
        //Zona de explosión
        GameObject explosion = Instantiate(FireballExplosionVolume, transform.position + transform.forward * 1.8f, Quaternion.identity);
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
