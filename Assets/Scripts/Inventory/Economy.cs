using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Economy : MonoBehaviour
{
    static public Economy instance;

    //Parametros guardables

    public uint currency { get; private set; }

    public uint gems { get; private set; }

    private void Awake()
    {
        #region "Singleton"

        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        #endregion
    }

    public void RewardCurrency(uint add)
    {
        currency += add;
    }

    public void RewardGems(uint add)
    {
        gems += add;
    }

    public void SpendCurrency(uint add)
    {
        currency -= add;
    }

    public void SpendGems(uint add)
    {
        gems -= add;
        GameData.Gems = (int)gems;
    }
}
