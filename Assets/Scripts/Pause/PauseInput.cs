using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseInput : MonoBehaviour
{
    [SerializeField]
    CanvasGroup pauseMenu;
    bool toggle = false;

    private void Awake()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale != 0 || Input.GetKeyDown(KeyCode.Escape) && pauseMenu.alpha == 1)
        {
            toggle = !toggle;
            ActiveCursor(toggle);
            Inventory.instance.TimeChange(toggle);
        }

        pauseMenu.interactable = toggle;
        pauseMenu.blocksRaycasts = toggle;

        if (toggle)
        {
            pauseMenu.alpha = 1;
        }
        else
        {
            pauseMenu.alpha = 0;
        }
    }

    void ActiveCursor(bool callBack)
    {
        // Si, ya no hace nada esto
        // No, no voy a quitar el metodo
        // Perdoname Gio
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    public void ManualSave()
    {
        Debug.Log("Guardado");
    }

    public void MainMenu()
    {
        toggle = !toggle;
        Inventory.instance.TimeChange(toggle);

        SceneManager.LoadScene(1);
        Debug.Log("Transladando a lobby");
    }
}
