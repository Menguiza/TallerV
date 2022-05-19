using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseInput : MonoBehaviour
{
    [SerializeField]
    CanvasGroup pauseMenu;
    bool toggle = false;

    [SerializeField] GameObject guideCanvas;
    [SerializeField] GameObject soundCanvas;

    private void Awake()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (guideCanvas.activeInHierarchy) // Ya está activo
            {
                toggle = !toggle;
                ActiveCursor(toggle);
                Inventory.instance.TimeChange(toggle);

                guideCanvas.SetActive(false);
                soundCanvas.SetActive(false);

                if (toggle) SoundManager.instance.SetPauseMusic();
                else SoundManager.instance.SetUnpausedMusic();

                return;
            }

            guideCanvas.SetActive(true);
            soundCanvas.SetActive(false);

            if (Time.timeScale == 0) return; // Ya está pausado

            toggle = !toggle;
            ActiveCursor(toggle);
            Inventory.instance.TimeChange(toggle);

            if (toggle) SoundManager.instance.SetPauseMusic();
            else SoundManager.instance.SetUnpausedMusic();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale != 0 || Input.GetKeyDown(KeyCode.Escape) && pauseMenu.alpha == 1)
        {
            if (guideCanvas.activeInHierarchy || soundCanvas.activeInHierarchy)
            {
                guideCanvas.SetActive(false);
                soundCanvas.SetActive(false);
                return;
            }

            toggle = !toggle;
            ActiveCursor(toggle);
            Inventory.instance.TimeChange(toggle);

            guideCanvas.SetActive(false);
            soundCanvas.SetActive(false);

            if (toggle) SoundManager.instance.SetPauseMusic();
            else SoundManager.instance.SetUnpausedMusic();
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
        GameMaster.instance.OnRunEnd.Invoke();

        SceneManager.LoadScene(2);
        Debug.Log("Transladando a lobby");
    }

    public void ToActualMainMenu()
    {
        toggle = !toggle;
        Inventory.instance.TimeChange(toggle);

        SceneManager.LoadScene(1);
        Debug.Log("Transladando al menu principal");
    }
}
