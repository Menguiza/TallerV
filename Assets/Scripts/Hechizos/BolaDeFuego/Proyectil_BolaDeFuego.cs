using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil_BolaDeFuego : MonoBehaviour
{
    public float damage;
    [SerializeField] GameObject FireballExplosionVolume;

    [SerializeField] GameObject trailF;
    [SerializeField] GameObject particles;

    public void fireballExplode()
    {
        //Zona de explosión
        GameObject explosion = Instantiate(FireballExplosionVolume, transform.position, Quaternion.identity);
        explosion.GetComponent<ExplosionBolaDeFuego>().damage = damage;

        //Aqui van las particulas --

        //Separar el trail
        
        trailF.GetComponent<TrailRenderer>().emitting = false;
        var emmision = particles.GetComponent<ParticleSystem>().emission;
        emmision.enabled = false;

        trailF.transform.SetParent(null);
        particles.transform.SetParent(null);

        

        Destroy(trailF, 4f);
        Destroy(particles, 4f);

        //Siempre Eliminar al final el proyectil
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider collider)
    {
        fireballExplode();
    }
}
