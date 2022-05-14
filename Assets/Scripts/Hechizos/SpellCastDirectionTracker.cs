using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCastDirectionTracker : MonoBehaviour
{
    Vector3 screenPos;
    Vector3 worldPos;
    [SerializeField] Transform refObject;

    private void Start()
    {
        screenPos.z = Vector3.Distance(GameMaster.instance.playerObject.transform.position, Camera.main.transform.position);
    }

    void Update()
    {
        screenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPos.z);
        
        worldPos = Camera.main.ScreenToWorldPoint(screenPos);
        refObject.position = worldPos;

        print("SP (X:" + screenPos.x + " Y:" + screenPos.y + ") WP (X:" + worldPos.x + " Y:" + worldPos.y + " Z:" + worldPos.z + ")");
    }
}
