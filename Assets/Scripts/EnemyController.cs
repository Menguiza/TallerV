using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    uint life = 100;

    [SerializeField]
    GameObject vida;

    float result;
    float divisor = 100f;

    public uint Life 
    { 
        get => life; 
        set
        {
            if(value>0)
            {
                life = value;
            }
            else if(value<=0)
            {
                life = 0;
                Destroy(gameObject);
            }
        }
    }

    private void Update()
    {
        result = (float)life / divisor;
        vida.GetComponent<Image>().fillAmount = result;
    }
}
