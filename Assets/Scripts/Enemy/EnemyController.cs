using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class EnemyController : MonoBehaviour, IEnemy
{
    [SerializeField]
    uint life = 100;

    [SerializeField] int damage;
    public int Damage { get => damage; set => damage = value; }

    [SerializeField] int consciencia;
    public int Conciencia { get => consciencia; set => consciencia = value; }

    public uint conciencia = 5;

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
                GameMaster.instance.OnRoomFinished.Invoke();
                Destroy(gameObject);
            }
        }
    }

    private void Update()
    {
        result = (float)life / divisor;
        vida.GetComponent<Image>().fillAmount = result;
    }

    public void ReceiveDamage(int damage)
    {
        Life = (uint)(Mathf.Max(0, Life - damage));
    }

    public void DestroyEnemy()
    {
        // ?
    }
}
