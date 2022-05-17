using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCrate : MonoBehaviour, IEnemy
{
    [SerializeField] int health;

    [SerializeField] int damage;
    public int Damage { get => damage; set => damage = value; }

    [SerializeField] int conciencia;
    public int Conciencia { get => conciencia; set => conciencia = value; }

    [SerializeField] GameObject CrateExplotion;

    public void DestroyEnemy()
    {
        // Otorgar un objeto
        Inventory.instance.AddItem(Inventory.instance.GenerateRandomItem());
        Inventory.instance.OnItemCollected.Invoke();

        GameObject instance = Instantiate(CrateExplotion, transform.position, Quaternion.identity);
        Destroy(instance, 4f);

        Destroy(gameObject);
    }

    public void ReceiveDamage(int dmg)
    {
        health -= dmg;
        if (health <= 0) DestroyEnemy();
    }
}
