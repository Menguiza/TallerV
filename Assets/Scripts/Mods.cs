using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mods
{
    //Info
    private string name;
    private bool utilizado;

    //Parametros
    private sbyte multVidaMax;
    private sbyte multDmg;
    private float multConciencia;
    private sbyte multTGPC;
    private sbyte multCritProb;
    private float multCrit;
    private sbyte multRoboPer;
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
    public sbyte MultVidaMax
    { 
        get => multVidaMax; 
        set
        {
            multVidaMax = value;
        }
    }
    public sbyte MultDmg 
    { 
        get => multDmg; 
        set
        {
            multDmg = value;
        }
    }
    public float MultConciencia 
    { 
        get => multConciencia;
        set
        {
            multConciencia = value;
        }
    }
    public sbyte MultTGPC 
    { 
        get => multTGPC; 
        set
        {
            multTGPC = value;
        }
    }
    public sbyte MultCritProb 
    { 
        get => multCritProb; 
        set
        {
            multCritProb = value;
        }
    }
    public float MultCrit 
    { 
        get => multCrit; 
        set
        {
            multCrit = value;
        }
    }
    public sbyte MultRoboPer 
    { 
        get => multRoboPer;
        set
        {
            multRoboPer = value;
        }
    }
    public float MultVelAtaque 
    { 
        get => multVelAatque;
        set
        {
            multVelAatque = value;
        }
    }
    public float MultSpeed 
    { 
        get => multSpeed;
        set
        {
            multSpeed = value;
        }
    }

    //Constructor
    public Mods(string name, sbyte multVidaMax, sbyte multDmg, float multiplicadorConciencia, sbyte multTGPC, sbyte multCritProb, float multCrit
        , sbyte multRoboPer, float multRobo, float multSpeed)
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
