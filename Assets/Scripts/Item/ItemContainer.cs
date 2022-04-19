using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemContainer : MonoBehaviour
{
    public Item itemInfo;
    public Image icon;
    public TMP_Text count;
    public int counter;

    private void Start()
    {
        if(itemInfo != null)
        {
            icon.sprite = itemInfo.icon;
        }
        counter = 1;
    }

    private void Update()
    {
        if(count != null)
        {
            count.text = "x" + counter;
        }

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
}
