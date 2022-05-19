using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAllOnArriveToLobby : MonoBehaviour
{
    private void Start()
    {
        Invoke(nameof(LateReset), 0.2f);
    }

    void LateReset()
    {
        GameMaster.instance.RemoveActiveTechniques();
        Inventory.instance.ResetInventory();
        GameMaster.instance.RemoveAllMods();

        print("Late reset");
    }
}
