using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VespulaSpawner : MonoBehaviour, IEnemy
{
    public float timeToSpawn;
    float timer, health = 5;

    [SerializeField]
    GameObject prefab;

    [SerializeField]
    int dmg, conciencia;

    public int Damage { get => dmg; set => dmg = value; }
    public int Conciencia { get => conciencia; set => conciencia = value; }

    private void Update()
    {
        if (transform.childCount <2)
        {
            timer += Time.deltaTime;

            if (timer >= timeToSpawn)
            {
                // Spawnear enemigo
                Spawn();
                timer = 0;
            }
        }
    }

    void Spawn()
    {
        Instantiate(prefab, transform.position, Quaternion.identity, transform);
    }


    [SerializeField] GameObject GetHitParticle;

    public void ReceiveDamage(int dmg)
    {
        health -= dmg;

        //VFX
        GameObject instanceParticle = Instantiate(GetHitParticle, transform.position + Vector3.up, Quaternion.identity);
        Destroy(instanceParticle, 4f);

        if (health <= 0) DestroyEnemy();
    }

    public void DestroyEnemy()
    {
        for(int i = 0; i<transform.childCount; i++)
        {
            transform.GetChild(i).parent = null;
        }

        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            GameMaster.instance.DamagePlayer(Damage, Conciencia);
        }
    }
}
