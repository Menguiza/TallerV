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

        currency = 0;

        gems = 0;
    }

    public void Reward()
    {
        currency += 1;
        gems += 1;
    }
}
