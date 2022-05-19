using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CursorTextChange : MonoBehaviour
{
    Vector2 hotSpot = new Vector2(0, -10);
    GameObject temp;
    [SerializeField] Canvas mainCanvas;
    [SerializeField] GameObject text, uiSpawn;

    public void OnButtonEnter()
    {
        Cursor.SetCursor(GameMaster.instance.cursorTexture, hotSpot, CursorMode.Auto);
    }

    public void OnButtonExit()
    {
        Cursor.SetCursor(GameMaster.instance.cursorTexture2, hotSpot, CursorMode.Auto);
    }

    public void PopUpInfo(int num)
    {
        if (Inventory.instance.activosInv[num].GetComponent<ItemContainerInv>().itemInfo == null)
        {
            return;
        }
            
        temp = Instantiate(text, Input.mousePosition, Quaternion.identity, uiSpawn.transform);
        temp.GetComponent<TMP_Text>().text = Inventory.instance.activosInv[num].GetComponent<ItemContainerInv>().itemInfo.name;
    }

    public void PopUpInfoSpells(int num)
    {
        if (ManagerHechizos.instance.spellsData[num] == null)
        {
            return;
        }

        temp = Instantiate(text, Input.mousePosition, Quaternion.identity, uiSpawn.transform);
        temp.GetComponent<TMP_Text>().text = ManagerHechizos.instance.spellsData[num].spellName;
    }

    public void DestroyPopUp()
    {
        if(temp != null)
        {
            Destroy(temp);
        }
    }
}
