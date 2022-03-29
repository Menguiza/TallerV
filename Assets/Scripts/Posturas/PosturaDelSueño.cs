using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NuevaPostura", menuName = "ScriptableObjects/Postura", order = 1)]
public class PosturaDelSueño : ScriptableObject
{
    [Header("Other")]
    public string stanceName;
    public Sprite icon;
    public List<ModsTecnicas> Techniques;
    public string desc;
}
