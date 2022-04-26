using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorTextChange : MonoBehaviour
{
    Vector2 hotSpot = new Vector2(0, -10);

    public void OnButtonEnter()
    {
        Cursor.SetCursor(GameMaster.instance.cursorTexture, hotSpot, CursorMode.Auto);
    }

    public void OnButtonExit()
    {
        Cursor.SetCursor(GameMaster.instance.cursorTexture2, hotSpot, CursorMode.Auto);
    }
}
