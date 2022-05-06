using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class MainMenuInterface : MonoBehaviour
{
    public void NuevaPartida()
    {
        GameData.SaveGameData(); // Esto creará una nueva partida
        SceneManager.LoadScene(2);
    }

    public void ContinuarPartida()
    {
        if (File.Exists(Application.persistentDataPath + "/DwizardSave.dat"))
        {
            SceneManager.LoadScene(2);
        }
    }

    public void Salir()
    {
        Application.Quit();
    }
}
