using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Reward", menuName = "ScriptableObjects/Reward", order = 1)]

public class RewardInfo : ScriptableObject
{
    public uint coins, gems;
    public ItemType type;
    Item item = null;

    public Sprite Give()
    {
        Economy.instance.RewardCurrency(coins);
        Economy.instance.RewardGems(gems);

        if(type == ItemType.Activo || type == ItemType.Pasivo)
        {
            item = Inventory.instance.GenerateRandomItem(type);
            Inventory.instance.AddItem(item);
            return item.icon;
        }

        return null;
    }
}
