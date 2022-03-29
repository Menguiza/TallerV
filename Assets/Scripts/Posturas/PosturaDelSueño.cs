using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NuevaPostura", menuName = "ScriptableObjects/Postura", order = 1)]
public class PosturaDelSueño : ScriptableObject
{
    [Header("Other")]
    [SerializeField] Sprite icon;
    [Header("Sleep technique modifier")]
    [SerializeField] string ST_name;
    [SerializeField] sbyte ST_multVidaMax;
    [SerializeField] sbyte ST_multDmg;
    [SerializeField] float ST_multConciencia;
    [SerializeField] sbyte ST_multTGPC;
    [SerializeField] sbyte ST_multCritProb;
    [SerializeField] float ST_multCrit;
    [SerializeField] sbyte ST_multRoboPer;
    [SerializeField] float ST_multVelAatque;
    [SerializeField] float ST_multSpeed;
    [SerializeField] sbyte ST_multPesadillaPer;

    [Header("Dream technique modifier")]
    [SerializeField] string DT_name;
    [SerializeField] sbyte DT_multVidaMax;
    [SerializeField] sbyte DT_multDmg;
    [SerializeField] float DT_multConciencia;
    [SerializeField] sbyte DT_multTGPC;
    [SerializeField] sbyte DT_multCritProb;
    [SerializeField] float DT_multCrit;
    [SerializeField] sbyte DT_multRoboPer;
    [SerializeField] float DT_multVelAatque;
    [SerializeField] float DT_multSpeed;
    [SerializeField] sbyte DT_multPesadillaPer;
}
