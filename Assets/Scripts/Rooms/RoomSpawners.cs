using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomSpawners", menuName = "ScriptableObjects/RoomSpawners", order = 1)]
public class RoomSpawners : ScriptableObject
{
    public List<GameObject> objects;
    public List<Vector3> spawns;
}
