using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPC_Posturas : MonoBehaviour, IVendorNPC
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

    void Start()
    {
        boughtStances = new bool[sleepStances.Length];

        // Inicializar UI de costos basándose en los datos de cada ScriptableObject de las posturas
        // Inicializar UI de desc                                        ^
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

    public void RetrieveBoughtStancesData()
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
            Economy.instance.Reward(500);
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
        }   
    }

    public void OpenStore()
    {
        vendorUI.SetActive(true);

        // Mostrar los SetButtons correctos basado en si la postura ya está comprada
        for (int i = 0; i < sleepStances.Length; i++)
        {
            DisplaySetButton(i, boughtStances[i]);
            DisplayBuyButton(i, !boughtStances[i]);
        }
    }

    public void CloseStore()
    {
        vendorUI.SetActive(false);
    }
    #endregion
}
