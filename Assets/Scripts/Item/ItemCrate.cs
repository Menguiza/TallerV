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

    [SerializeField] bool markedForActiveInTutorial;
    [SerializeField] bool markedForPasiveInTutorial;
    [SerializeField] Item sarten;
    [SerializeField] Item anillo;

    public void DestroyEnemy()
    {
        ItemType itemType;

        if (!markedForActiveInTutorial && !markedForPasiveInTutorial)
        {
            int randomChance = Random.Range(1, 101);
            

            if (randomChance < 75) itemType = ItemType.Pasivo;
            else itemType = ItemType.Activo;

            // Otorgar un objeto
            Inventory.instance.AddItem(Inventory.instance.GenerateRandomItem(itemType));
            Inventory.instance.OnItemCollected.Invoke();

            GameObject instance = Instantiate(CrateExplotion, transform.position, Quaternion.identity);
            Destroy(instance, 4f);

            Destroy(gameObject);
        }
        else
        {
            if (markedForActiveInTutorial)
            {
                Inventory.instance.AddItem(sarten);
                Inventory.instance.OnItemCollected.Invoke();

                GameObject instance = Instantiate(CrateExplotion, transform.position, Quaternion.identity);
                Destroy(instance, 4f);

                Destroy(gameObject);
            }
            else
            {
                Inventory.instance.AddItem(anillo);
                Inventory.instance.OnItemCollected.Invoke();

                GameObject instance = Instantiate(CrateExplotion, transform.position, Quaternion.identity);
                Destroy(instance, 4f);

                Destroy(gameObject);
            }
        }
        

        
    }

    public void ReceiveDamage(int dmg)
    {
        health -= dmg;
        if (health <= 0) DestroyEnemy();
    }
}
