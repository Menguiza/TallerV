using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Rarity
{
    Common,
    Complex,
    Domain,
    Forbidden
}

[CreateAssetMenu(fileName = "NuevaPostura", menuName = "ScriptableObjects/Postura", order = 1)]
public class PosturaDelSueño : ScriptableObject
{
    [Header("Other")]
    public string stanceName;
    public Sprite icon;
    public List<ModsTecnicas> Techniques;
    public string desc;
    public Rarity rarity;
}
