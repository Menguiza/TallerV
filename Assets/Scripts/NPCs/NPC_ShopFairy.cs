using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPC_ShopFairy : MonoBehaviour, IVendorNPC
{
    Item[] shopItems;
    bool[] boughtItems;

    bool isStoreOpen;
    public bool IsStoreOpen { get => isStoreOpen; set => isStoreOpen = value; }

    void Start()
    {
        //Initialize store
        shopItems = new Item[6];
        boughtItems = new bool[6];

        shopItems[0] = Inventory.instance.GenerateRandomItem(ItemType.Activo);
        shopItems[1] = Inventory.instance.GenerateRandomItem(ItemType.Pasivo);
        shopItems[2] = Inventory.instance.GenerateRandomItem(ItemType.Activo);
        shopItems[3] = Inventory.instance.GenerateRandomItem(ItemType.Pasivo);
        shopItems[4] = Inventory.instance.GenerateRandomItem(ItemType.Pasivo);
        shopItems[5] = Inventory.instance.GenerateRandomItem(ItemType.Pasivo);
    }

    public void Buy(int itemIndex)
    {

    }

    public void CloseStore()
    {

    }

    public void OpenStore()
    {

    }
}
