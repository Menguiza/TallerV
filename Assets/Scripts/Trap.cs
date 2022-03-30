using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Trap", menuName = "Trap", order = 1)]
public class Trap : ScriptableObject
{
    public TrapType type;
    public float offsetInY;
    public byte damage;
}

public enum TrapType { Spikes };

[CreateAssetMenu(fileName = "Item", menuName = "Item", order = 1)]
public class Item : ScriptableObject
{
    public GameMaster gm;

    [Header("Info.")]

    public string nombre = "";
    public ItemType type;
    public ItemFormat format;

    [Header("Parametros")]

    public sbyte multVidaMax = 0;
    public sbyte multDmg = 0;
    public float multConciencia = 0;
    public sbyte multTGPC = 0;
    public sbyte multCritProb = 0;
    public float multCrit = 0;
    public sbyte multRoboPer = 0;
    public float multVelAatque = 0;
    public float multSpeed = 0;
    public sbyte multPesadillaPer = 0;

    [Header("Impacto")]

    public sbyte sumVida = 0;
    public sbyte sumConciencia = 0;
    public byte sumDinero = 0;

    public void AddParameters()
    {
        gm.AddMod(name, multVidaMax, multDmg, multConciencia, multTGPC, multCritProb, multCrit, multRoboPer, multVelAatque, multSpeed, multPesadillaPer);
    }

    public void Impact()
    {
        gm.DamagePlayer((sumVida));
        gm.Player.Conciencia = (ushort)Mathf.Max(0, gm.Player.Conciencia + sumConciencia);
        //Sumar monedas
    }
}

public enum ItemType { Sarten, Pocion, Pasivo };

public enum ItemFormat { Stackeable, Unique };
