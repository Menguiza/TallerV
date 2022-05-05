using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Room", menuName = "ScriptableObjects/Room", order = 1)]
public class Room : ScriptableObject
{
    public List<RoomSpawners> data;
}