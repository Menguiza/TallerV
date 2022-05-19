using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAllOnArriveToLobby : MonoBehaviour
{
    private void Start()
    {
        GameMaster.instance.RemoveActiveTechniques();
        // Reset items 
    }
}
