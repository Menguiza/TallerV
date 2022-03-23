using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    private uint maxLife;
    private uint life;
    private uint damage;
    private GameMaster.estado status = GameMaster.estado.Despierto;
    private uint tGPC = 1;
    private byte critProb = 0;
    private float critMult = 3.0f;
    private byte roboVida = 0;
    private float multVelAtaque = 1f;
    private float speedMult = 1f;

    [SerializeField]
    private ushort maxConciencia = 100;

    private ushort conciencia;

    public uint MaxLife 
    { 
        get => maxLife;

        set
        {
            maxLife = value;
        }
    }

    public uint Life 
    { 
        get => life;

        set
        {
            if (value > 0 && value < maxLife)
            {
                life = value;
            }
            else if (value <= 0)
            {
                life = 0;
            }
            else if(value >= maxLife)
            {
                life = maxLife;
            }
            else
            {
                Debug.LogWarning("Error al dar valor a la vida actual del jugador.");
            }
        }
    }
    public uint Damage 
    { 
        get => damage; 
        set
        {
            if(value>=0)
            {
                damage = value;
            }
            else
            {
                Debug.LogWarning("Se esta intentando asiganr un daño negativo.");
            }
        }
    }
    public ushort MaxConciencia { get => maxConciencia; }
    public ushort Conciencia 
    { 
        get => conciencia; 
        set
        {
            if (value <= maxConciencia && value > 0)
            {
                conciencia = value;
            }
            else if (value <= 0)
            {
                conciencia = 0;
                Status = GameMaster.estado.Dormido;
            }
            else if (value >= maxConciencia)
            {
                Status = GameMaster.estado.Despierto;
                conciencia = maxConciencia;
            }
            else
            {
                Debug.Log("Error al dar valor a la conciencia del jugador ");
            }
        }
    }

    public GameMaster.estado Status { get => status; set => status = value; }
    public uint TGPC 
    { 
        get => tGPC;
        set
        {
            if(value >= 0 && value < maxLife)
            {
                tGPC = value;
            }
            else if(value >= maxLife)
            {
                tGPC = maxLife;
            }
            else
            {
                Debug.LogWarning("Se esta asignando al TGPC un valor negativo o mayor a la vida maxima.");
            }
        }
    }

    public byte CritProb 
    { 
        get => critProb; 
        set
        {
            if(value>=0 && value<=100)
            {
                critProb = value;
            }
            else if(value>100)
            {
                critProb = 100;
            }
        }
    }

    public float CritMult 
    { 
        get => critMult; 
        set
        {
            if (value >= 1)
            {
                critMult = value;
            }
            else
            {
                Debug.LogWarning("Error al dar valor a Multiplicador de critico.");
            }
        }
    }

    public byte RoboVida 
    { 
        get => roboVida; 
        set
        {
            if (value >= 0 && value <= 100)
            {
                roboVida = value;
            }
            else if (value > 100)
            {
                roboVida = 100;
            }
            else
            {
                Debug.LogWarning("Error al dar valor a Robo de Vida.");
            }
        }
    }

    public float MultVelAtaque
    { 
        get => multVelAtaque; 
        set
        {
            if(value >= 1)
            {
                multVelAtaque = value;
            }
            else
            {
                Debug.LogWarning("Error al dar valor al multiplicador de Velocidad de Ataque.");
            }
        }
    }

    public float SpeedMult 
    { 
        get => speedMult; 
        set
        {
            if (value >= 0)
            {
                speedMult = value;
            }
            else
            {
                Debug.LogWarning("Se esta intentando asignar un multiplicador de velocidad negativo.");
            }
        }
    }

    public Player(uint maxLife, uint damage)
    {
        MaxLife = maxLife;
        Life = MaxLife;
        Damage = damage;
        Conciencia = MaxConciencia;
    }
}
