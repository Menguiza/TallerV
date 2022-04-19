using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Trap", menuName = "ScriptableObjects/Trap", order = 1)]
public class Trap : ScriptableObject
{
    public TrapType type;
    public float offsetInY;
    public byte damage;
}

public enum TrapType { Spikes };
