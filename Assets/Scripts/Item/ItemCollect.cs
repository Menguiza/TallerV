using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollect : MonoBehaviour
{
    public Item item;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>() != null && GameMaster.instance.playerObject != null)
        {
            Inventory.instance.AddItem(item);
            Inventory.instance.OnItemCollected.Invoke();
            Destroy(gameObject);
        }
    }
}
