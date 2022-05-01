using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemContainerInv : MonoBehaviour
{
    public Item itemInfo;
    public Image icon;

    Item nullReference;

    private void Start()
    {
        if (itemInfo != null)
        {
            icon.sprite = itemInfo.icon;
        }

        int result;

        if (int.TryParse(gameObject.name, out result))
        {
            Inventory.instance.activosInv[result - 1] = gameObject;
        }
    }

    private void Update()
    {
        if (itemInfo != null)
        {
            icon.sprite = itemInfo.icon;
            Color color = icon.color;
            color.a = 1;
            icon.color = color;
        }
        else
        {
            Color color = icon.color;
            color.a = 0;
            icon.color = color;
        }
    }

    public void RemoveReference()
    {
        itemInfo = nullReference;
    }
}
