using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryInput : MonoBehaviour
{
    [SerializeField]
    CanvasGroup inventory;
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

        inventory.interactable = toggle;

        if(inventory.interactable)
        {
            inventory.alpha = 1;
            Time.timeScale = 0;
        }
        else
        {
            inventory.alpha = 0;
            Time.timeScale = 1;
        }
    }
}
