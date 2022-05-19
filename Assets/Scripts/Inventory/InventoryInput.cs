using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryInput : MonoBehaviour
{
    [SerializeField]
    CanvasGroup inventory;
    Animator anim;
    bool toggle = false;

    [SerializeField] AudioClip openInventorySound;
    [SerializeField] AudioClip closeInventorySound;
    AudioSource audioSource;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    void Update()
    {
        if(Input.GetButtonDown("Inventory") && Time.timeScale != 0 || Input.GetButtonDown("Inventory") && inventory.alpha == 1)
        {
            toggle = !toggle;
            ActiveCursor(toggle);
            Inventory.instance.TimeChange(toggle);

            if (Time.timeScale == 0) audioSource.PlayOneShot(openInventorySound);
            else audioSource.PlayOneShot(closeInventorySound);
        }

        anim.SetBool("Abierto", toggle);

        inventory.interactable = toggle;
        inventory.blocksRaycasts = toggle;

        if (inventory.interactable)
        {
            inventory.alpha = 1;
        }
        else
        {
            inventory.alpha = 0;
        }
    }

    public void ActiveCursor(bool callBack)
    {
        // Si, esto ahora no hará nada ya que el cursor siempre será visible
        // No, no quiero borrar esto porque me tocaria arreglar un par de cosas que no estoy dispuesto a arreglar
        // Gracias
    }
}
