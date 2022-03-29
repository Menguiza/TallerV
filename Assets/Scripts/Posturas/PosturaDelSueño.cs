using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NuevaPostura", menuName = "ScriptableObjects/Postura", order = 1)]
public class PosturaDelSueño : ScriptableObject
{
    [Header("Other")]
    [SerializeField] Sprite icon;
    [Header("Sleep techniques")]
    public List<ModsTecnicas> Techniques;

}
