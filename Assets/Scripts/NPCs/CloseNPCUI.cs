using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseNPCUI : MonoBehaviour
{
    [SerializeField] GameObject vendorNPC;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            vendorNPC.GetComponent<IVendorNPC>().CloseStore();
        }
    }
}
