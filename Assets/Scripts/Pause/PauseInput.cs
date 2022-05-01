using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseInput : MonoBehaviour
{
    [SerializeField]
    CanvasGroup pauseMenu;
    bool toggle = false;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            toggle = !toggle;
            ActiveCursor(toggle);
        }

        pauseMenu.interactable = toggle;
        pauseMenu.blocksRaycasts = toggle;

        if (pauseMenu.interactable)
        {
            pauseMenu.alpha = 1;
            Time.timeScale = 0;
        }
        else
        {
            pauseMenu.alpha = 0;
            Time.timeScale = 1;
        }
    }

    void ActiveCursor(bool callBack)
    {
        if(callBack)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
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
        Debug.Log("Transladando a lobby");
    }
}
