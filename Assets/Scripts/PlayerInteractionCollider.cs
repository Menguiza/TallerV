using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionCollider : MonoBehaviour
{
    private void Awake()
    {
        Destroy(gameObject, 0.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<IInteractive>() != null)
        {
            if (other.gameObject.GetComponent<IVendorNPC>() != null)
            {
                if (!other.gameObject.GetComponent<IVendorNPC>().IsStoreOpen) other.gameObject.GetComponent<IVendorNPC>().OpenStore();
                else other.gameObject.GetComponent<IVendorNPC>().CloseStore();
            }

            Destroy(gameObject);
        }
    }
}
