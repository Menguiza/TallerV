using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;

public class MainMenuInterface : MonoBehaviour
{
    [SerializeField] Button continueButton;

    private void Start()
    {
        if (File.Exists(Application.persistentDataPath + "/DwizardSave.dat")) continueButton.interactable = true;
        else continueButton.interactable = false;
    }

    private void Update()
    {
        // Debug
        if ( Input.GetKeyDown(KeyCode.P))
        {
            GameData.ResetSaveFile();
        }
    }

    public void NuevaPartida()
    {
        GameData.ResetSaveFile();
        GameData.SaveGameData(); 
        SceneManager.LoadScene(2);
        GameData.LoadGameData();
        Economy.instance.ResetCurrencyAndGems();
        Economy.instance.RewardGems((uint)GameData.RetreiveGems());
    }

    public void ContinuarPartida()
    {
        if (File.Exists(Application.persistentDataPath + "/DwizardSave.dat"))
        {
            SceneManager.LoadScene(2);
            GameData.LoadGameData();
            Economy.instance.RewardGems((uint)GameData.RetreiveGems());
        }
    }

    public void Salir()
    {
        Application.Quit();
    }
}
