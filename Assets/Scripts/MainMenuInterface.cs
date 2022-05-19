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
        DontDestroyOnLoad(gameObject);

        #region"Clean singletons"
        GameMaster gm = GameMaster.instance;
        Economy ec = Economy.instance;
        Inventory inv = Inventory.instance;
        ManagerHechizos mh = ManagerHechizos.instance;
        SoundManager sm = SoundManager.instance;
        RoomManager rm = RoomManager.instance;

        if (gm != null)
        {
            gm.StopAllCoroutines();
            gm.CancelInvoke();
            Destroy(gm.gameObject);
            
        }
        if (ec != null)
        {
            ec.StopAllCoroutines();
            ec.CancelInvoke();
            Destroy(ec.gameObject);
        }
        if (inv != null)
        {
            inv.StopAllCoroutines();
            inv.CancelInvoke();
            Destroy(inv.gameObject);
        }
        if (mh != null)
        {
            mh.StopAllCoroutines();
            mh.CancelInvoke();
            Destroy(mh.gameObject);
        }
        if (sm != null)
        {
            sm.StopAllCoroutines();
            sm.CancelInvoke();
            Destroy(sm.gameObject);
        }
        if (rm != null)
        {
            rm.StopAllCoroutines();
            rm.CancelInvoke();
            Destroy(rm.gameObject);
        }
        #endregion

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
        SceneManager.LoadScene(3);
        GameData.LoadGameData();
        StartCoroutine(InitializeOnNextFrame());
    }

    public void ContinuarPartida()
    {
        if (File.Exists(Application.persistentDataPath + "/DwizardSave.dat"))
        {
            SceneManager.LoadScene(2);
            GameData.LoadGameData();
            StartCoroutine(InitializeOnNextFrame());
        }
    }

    public void Salir()
    {
        Application.Quit();
    }

    IEnumerator InitializeOnNextFrame()
    {
        yield return null;
        Economy.instance.ResetCurrencyAndGems();
        Economy.instance.RewardGems((uint)GameData.RetreiveGems());
        Destroy(gameObject);
    }
}
