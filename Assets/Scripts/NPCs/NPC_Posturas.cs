using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPC_Posturas : MonoBehaviour, IVendorNPC, IInteractive
{
    [SerializeField] PosturaDelSueño[] sleepStancesData;
    [SerializeField] Postura[] sleepStances;
    bool[] boughtStances;

    [SerializeField] GameObject vendorUI;
    [SerializeField] GameObject[] setStanceUI;
    [SerializeField] GameObject[] buyStanceUI;
    [SerializeField] TextMeshProUGUI[] descUI;
    [SerializeField] TextMeshProUGUI[] costUI;
    [SerializeField] TextMeshProUGUI[] nameUI;
    [SerializeField] Image[] iconUI;

    [SerializeField] InventoryInput inventoryInput;

    bool isStoreOpen;
    public bool IsStoreOpen { get => isStoreOpen; set => isStoreOpen = value; }

    void Start()
    {
        boughtStances = new bool[6];

        RetrieveBoughtStancesData(GameData.RetreiveBoughtStances());

        // Inicializar UI basándose en los datos de cada ScriptableObject de las posturas
        for (int i = 0; i < sleepStances.Length; i++)
        {
            costUI[i].text = sleepStancesData[i].gemCost.ToString();
            descUI[i].text = sleepStancesData[i].desc;
            nameUI[i].text = sleepStancesData[i].stanceName;
            iconUI[i].sprite = sleepStancesData[i].icon;
            DisplaySetButton(i, boughtStances[i]);
            DisplayBuyButton(i, !boughtStances[i]);
        }
    }

    public void RetrieveBoughtStancesData(bool[] boughtStancesData)
    {
        for (int i = 0; i < boughtStancesData.Length; i++)
        {
            boughtStances[i] = boughtStancesData[i];
        }
    }

    public void SetStance(int stanceIndex)
    {
        GameMaster.instance.gameObject.GetComponent<InicializadorSistemaPosturas>().AssignNewStance(sleepStances[stanceIndex]);
    }

    void DisplaySetButton(int stanceIndex, bool state)
    {
        setStanceUI[stanceIndex].SetActive(state);
    }

    void DisplayBuyButton(int stanceIndex, bool state)
    {
        buyStanceUI[stanceIndex].SetActive(state);
    }
    
    //Debug
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Economy.instance.RewardGems(500);
        }
    }

    #region"MetodosDeIVendorNPC"
    public void Buy(int stanceIndex)
    {
        if (Economy.instance.gems >= sleepStancesData[stanceIndex].gemCost)
        {
            boughtStances[stanceIndex] = true;
            DisplaySetButton(stanceIndex, true);
            DisplayBuyButton(stanceIndex, false);

            Economy.instance.SpendGems((uint)sleepStancesData[stanceIndex].gemCost);
        }

        GameData.boughtStance0 = boughtStances[0] == false ? 0 : 1;
        GameData.boughtStance1 = boughtStances[1] == false ? 0 : 1;
        GameData.boughtStance2 = boughtStances[2] == false ? 0 : 1;
        GameData.boughtStance3 = boughtStances[3] == false ? 0 : 1;
        GameData.boughtStance4 = boughtStances[4] == false ? 0 : 1;
        GameData.boughtStance5 = boughtStances[5] == false ? 0 : 1;

        GameData.SaveGameData();
    }

    public void OpenStore()
    {
        vendorUI.SetActive(true);

        // Mostrar los botones correctos basado en si la postura ya está comprada
        for (int i = 0; i < sleepStances.Length; i++)
        {
            DisplaySetButton(i, boughtStances[i]);
            DisplayBuyButton(i, !boughtStances[i]);
        }

        inventoryInput.ActiveCursor(true);
        Inventory.instance.TimeChange(true);
        SoundManager.instance.SetPauseMusic();
        isStoreOpen = true;
    }

    public void CloseStore()
    {
        vendorUI.SetActive(false);

        inventoryInput.ActiveCursor(false);
        Inventory.instance.TimeChange(false);
        SoundManager.instance.SetUnpausedMusic();
        isStoreOpen = false;
    }
    #endregion
}
