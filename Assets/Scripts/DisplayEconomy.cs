using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayEconomy : MonoBehaviour
{
    public TMP_Text coins, gems;

    // Update is called once per frame
    void Update()
    {
        coins.text = Economy.instance.currency.ToString();
        gems.text = Economy.instance.gems.ToString();
    }
}
