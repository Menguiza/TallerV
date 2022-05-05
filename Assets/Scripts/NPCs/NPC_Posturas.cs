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
        boughtStances = new bool[sleepStances.Length];
        boughtStances[0] = true;

        // Inicializar UI basándose en los datos de cada ScriptableObject de las posturas                                    ^
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

    public void RetrieveBoughtStancesData(bool[] boughtStances)
    {
        // Recuperar la información de algún contenedor estático
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

        isStoreOpen = true;
    }

    public void CloseStore()
    {
        vendorUI.SetActive(false);

        inventoryInput.ActiveCursor(false);
        Inventory.instance.TimeChange(false);

        isStoreOpen = false;
    }
    #endregion
}
