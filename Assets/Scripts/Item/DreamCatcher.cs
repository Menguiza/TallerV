using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dreamcatcher", menuName = "ScriptableObjects/Dreamcatcher", order = 2)]
public class DreamCatcher : ScriptableObject
{
    [Header("Info.")]

    public string nombre = "";
    public Sprite icon;

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
    float multDañoRecibido = 0;

    public void AddParameters()
    {
        GameMaster.instance.AddMod(nombre, multVidaMax, multDmg, multConciencia, multTGPC, multCritProb, multCrit, multRoboPer, multVelAatque, multSpeed, multPesadillaPer, multDañoRecibido);
    }

    public void ResetParameter()
    {
        GameMaster.instance.RemoveMod(nombre);
    }
}
