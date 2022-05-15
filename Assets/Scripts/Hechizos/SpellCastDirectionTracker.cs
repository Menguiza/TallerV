using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCastDirectionTracker : MonoBehaviour
{
    Vector3 screenPos;
    Vector3 worldPos;

    private void Start()
    {
        screenPos.z = Vector3.Distance(GameMaster.instance.playerObject.transform.position, Camera.main.transform.position);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(worldPos, 0.5f);
    }

    void Update()
    {
        screenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPos.z);
        
        worldPos = Camera.main.ScreenToWorldPoint(screenPos);

        print("SP (X:" + screenPos.x + " Y:" + screenPos.y + ") WP (X:" + worldPos.x + " Y:" + worldPos.y + " Z:" + worldPos.z + ")");
    }
}
