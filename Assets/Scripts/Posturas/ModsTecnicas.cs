using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NuevaTecnica", menuName = "ScriptableObjects/Tecnica", order = 1)]
public class ModsTecnicas : ScriptableObject
{
    //Los estados en los que se aplica la postura
    enum ActiveState
    {
        awake,
        normalDream,
        nightmareDream,
        anyDream
    }

    [Header("Technique modifier values")]
    [SerializeField] ActiveState activeState;
    [SerializeField] string techniqueName;
    [SerializeField] sbyte multVidaMax;
    [SerializeField] sbyte multDmg;
    [SerializeField] float multConciencia;
    [SerializeField] sbyte multTGPC;
    [SerializeField] sbyte multCritProb;
    [SerializeField] float multCrit;
    [SerializeField] sbyte multRoboPer;
    [SerializeField] float multVelAatque;
    [SerializeField] float multSpeed;
    [SerializeField] sbyte multPesadillaPer;
}
