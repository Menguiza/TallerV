using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryInput : MonoBehaviour
{
    Animator anim;
    bool toggle = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(Input.GetButtonDown("Inventory"))
        {
            toggle = !toggle;
        }

        anim.SetBool("Abierto", toggle);
    }
}
