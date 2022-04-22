using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DreamContainer : MonoBehaviour
{
    [SerializeField]
    Image icon;

    // Update is called once per frame
    void Update()
    {
        if (Inventory.instance.dmrcatcher != null)
        {
            icon.sprite = Inventory.instance.dmrcatcher.icon;
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
}
