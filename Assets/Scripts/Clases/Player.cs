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
    private byte multPesadilla = 0;
    private bool pesadilla = false;
    private float multDañoRecibido = 1f;

    //Variables used for "Wake" and "Dream" UnityEvents
    public bool wakeFlag = false;
    public bool dreamFlag = false;

    [SerializeField]
    private ushort maxConciencia = 100;

    private ushort conciencia;

    public uint MaxLife 
    { 
        get => maxLife;

        set
        {
            if(value>=1)
            {
                maxLife = value;
            }
            else
            {
                maxLife = 1;
            }
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
                damage = 0;
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

                dreamFlag = true;
            }
            else if (value >= maxConciencia)
            {
                Status = GameMaster.estado.Despierto;
                conciencia = maxConciencia;
                //pesadilla = false;

                wakeFlag = true;
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
            if(value >= 0 && value < maxConciencia)
            {
                tGPC = value;
            }
            else if(value >= maxConciencia)
            {
                tGPC = maxConciencia;
            }
            else
            {
                TGPC = 0;
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
            else
            {
                critProb = 0;
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
                critMult = 1;
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
                roboVida = 0;
            }
        }
    }

    public float MultVelAtaque
    { 
        get => multVelAtaque; 
        set
        {
            if(value >= 0)
            {
                multVelAtaque = value;
            }
            else
            {
                multVelAtaque = 0;
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
                speedMult = 0;
            }
        }
    }

    public byte MultPesadilla 
    {
        get => multPesadilla;
        set
        {
            if (value >= 0 && value <= 100)
            {
                multPesadilla = value;
            }
            else if (value > 100)
            {
                multPesadilla = 100;
            }
            else
            {
                multPesadilla = 0;
            }
        }
    }

    public bool Pesadilla 
    { 
        get => pesadilla;
        set
        {
            /*
            if (value != pesadilla)
            {
                pesadilla = value;
            }*/
            pesadilla = value;
        }
    }

    public float MultDañoRecibido 
    { 
        get => multDañoRecibido; 
        set
        {
            if (value >= 0.25f)
            {
                multDañoRecibido = value;
            }
            else
            {
                multDañoRecibido = 0.25f;
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
