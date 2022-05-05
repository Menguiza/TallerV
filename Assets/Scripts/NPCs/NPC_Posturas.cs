using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Posturas : MonoBehaviour, IVendorNPC
{
    [SerializeField] PosturaDelSueño[] sleepStancesData;
    [SerializeField] Postura[] sleepStances;
    bool[] boughtStances;

    [SerializeField] GameObject vendorUI;
    [SerializeField] GameObject[] setStanceUI;
    [SerializeField] GameObject[] buyStanceUI;

    void Awake()
    {
        boughtStances = new bool[sleepStances.Length];
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
    


    #region"MetodosDeIVendorNPC"
    public void Buy(int stanceIndex)
    {
        boughtStances[stanceIndex] = true;
        DisplaySetButton(stanceIndex, true);
        DisplayBuyButton(stanceIndex, false);
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
