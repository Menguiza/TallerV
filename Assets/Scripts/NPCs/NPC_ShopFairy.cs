using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPC_ShopFairy : MonoBehaviour, IVendorNPC, IInteractive
{
    Item[] shopItems;

    bool isStoreOpen;
    public bool IsStoreOpen { get => isStoreOpen; set => isStoreOpen = value; }

    [SerializeField] GameObject ShopUI;
    [SerializeField] GameObject[] BuyPanel;
    [SerializeField] GameObject[] BoughtPanels;

    [SerializeField] TextMeshProUGUI[] NameText;
    [SerializeField] TextMeshProUGUI[] CostText;
    [SerializeField] Image[] Icons;

    void Start()
    {
        //Initialize store
        shopItems = new Item[6];

        shopItems[0] = Inventory.instance.GenerateRandomItem(ItemType.Activo);
        shopItems[1] = Inventory.instance.GenerateRandomItem(ItemType.Pasivo);
        shopItems[2] = Inventory.instance.GenerateRandomItem(ItemType.Activo);
        shopItems[3] = Inventory.instance.GenerateRandomItem(ItemType.Pasivo);
        shopItems[4] = Inventory.instance.GenerateRandomItem(ItemType.Pasivo);
        shopItems[5] = Inventory.instance.GenerateRandomItem(ItemType.Pasivo);

        for (int i = 0; i < shopItems.Length; i++)
        {
            NameText[i].text = shopItems[i].nombre;
            CostText[i].text = shopItems[i].price.ToString();
            Icons[i].sprite = shopItems[i].icon;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) Economy.instance.RewardCurrency(200);
    }

    public void Buy(int itemIndex)
    {
        if (shopItems[itemIndex].price > Economy.instance.currency)
        {
            BoughtPanels[itemIndex].SetActive(true);

            BuyPanel[itemIndex].SetActive(false);

            Economy.instance.SpendCurrency((uint)shopItems[itemIndex].price);

            Inventory.instance.AddItem(shopItems[itemIndex]);
        }
    }

    public void CloseStore()
    {
        ShopUI.SetActive(false); 
        Inventory.instance.TimeChange(false);
        SoundManager.instance.SetUnpausedMusic();

        isStoreOpen = false;
    }

    public void OpenStore()
    {
        isStoreOpen = true;
        ShopUI.SetActive(true);
        Inventory.instance.TimeChange(true);
        SoundManager.instance.SetPauseMusic();
    }
}
