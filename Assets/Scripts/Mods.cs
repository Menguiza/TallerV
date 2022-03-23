using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mods
{
    //Info
    private string name;
    private bool utilizado;

    //Parametros
    private byte multVidaMax;
    private byte multDmg;
    private float multConciencia;
    private byte multTGPC;
    private byte multCritProb;
    private float multCrit;
    private byte multRoboPer;
    private float multVelAatque;
    private float multSpeed;

    //Accesores
    public string Name 
    { 
        get => name; 
        set
        {
            if(!string.IsNullOrWhiteSpace(value) && !string.IsNullOrEmpty(value))
            {
                name = value;
            }
            else
            {
                name = "";
            }
        }
    }
    public bool Utilizado 
    { 
        get => utilizado; 
        set
        {
            if(value != utilizado)
            {
                utilizado = value;
            }
        }
    }
    public byte MultVidaMax
    { 
        get => multVidaMax; 
        set
        {
            if(value >= 0)
            {
                multVidaMax = value;
            }
            else
            {
                Debug.LogWarning("El multiplicador de vida que intenta ingresar sale de los limites.");
            }
        }
    }
    public byte MultDmg 
    { 
        get => multDmg; 
        set
        {
            if(value >= 0)
            {
                multDmg = value;
            }
            else
            {
                Debug.LogWarning("Se esta intentando asignar un valor inferior a 0 al multiplicador de daño.");
            }
        }
    }
    public float MultConciencia 
    { 
        get => multConciencia;
        set
        {
            if(value >= 0)
            {
                multConciencia = value;
            }
            else
            {
                Debug.LogWarning("El valor que intenta asignar al multiplicador de conciencia es inferior a 0.");
            }
        }
    }
    public byte MultTGPC 
    { 
        get => multTGPC; 
        set
        {
            if(value >= 0)
            {
                multTGPC = value;
            }
            else
            {
                Debug.LogWarning("Se esta intentando asignar un valor inferior a 0 al TGPC.");
            }
        }
    }
    public byte MultCritProb 
    { 
        get => multCritProb; 
        set
        {
            if(value >= 0)
            {
                multCritProb = value;
            }
            else
            {
                Debug.LogWarning("El valor que intenta asignar al multiplicador de la probabilidad de critico es inferior a 0.");
            }
        }
    }
    public float MultCrit 
    { 
        get => multCrit; 
        set
        {
            if(value >= 0)
            {
                multCrit = value;
            }
            else
            {
                Debug.LogWarning("El valor que intenta asignar al multiplicador de daño critico es inferior a 0.");
            }
        }
    }
    public byte MultRoboPer 
    { 
        get => multRoboPer;
        set
        {
            if (value >= 0)
            {
                multRoboPer = value;
            }
            else
            {
                Debug.LogWarning("El valor que intenta asignar al multiplicador percentual de robo de vida es inferior a 0.");
            }
        }
    }
    public float MultVelAtaque 
    { 
        get => multVelAatque;
        set
        {
            if (value >= 0)
            {
                multVelAatque = value;
            }
            else
            {
                Debug.LogWarning("El valor que intenta asignar al multiplicador de robo de vida es inferior a 0.");
            }
        }
    }
    public float MultSpeed 
    { 
        get => multSpeed;
        set
        {
            if (value >= 0)
            {
                multSpeed = value;
            }
            else
            {
                Debug.LogWarning("El valor que intenta asignar al multiplicador de velocidad es inferior a 0.");
            }
        }
    }

    //Constructor
    public Mods(string name, byte multVidaMax, byte multDmg, float multiplicadorConciencia, byte multTGPC, byte multCritProb, float multCrit
        , byte multRoboPer, float multRobo, float multSpeed)
    {
        Name = name;
        Utilizado = false;

        MultVidaMax = multVidaMax;
        MultDmg = multDmg;
        MultConciencia = multiplicadorConciencia;
        MultTGPC = multTGPC;
        MultCritProb = multCritProb;
        MultCrit = multCrit;
        MultRoboPer = multRoboPer;
        MultVelAtaque = multRobo;
        MultSpeed = multSpeed;
    }
}
