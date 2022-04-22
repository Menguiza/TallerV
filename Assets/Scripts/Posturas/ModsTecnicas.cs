using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NuevaTecnica", menuName = "ScriptableObjects/Tecnica", order = 1)]
public class ModsTecnicas : ScriptableObject
{
    //Los estados en los que se aplica la postura
    public enum ActiveState
    {
        awake,
        normalDream,
        nightmareDream,
        anyDream
    }

    [Header("Technique modifier values")]
    public ActiveState activeState;
    public string techniqueName;
    public sbyte multVidaMax;
    public sbyte multDmg;
    public float multConciencia;
    public sbyte multTGPC;
    public sbyte multCritProb;
    public float multCrit;
    public sbyte multRoboPer;
    public float multVelAatque;
    public float multSpeed;
    public sbyte multPesadillaPer;
    public float multDañoRecibido;
    public string desc;
}
