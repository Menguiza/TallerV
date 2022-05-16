using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCastDirectionTracker : MonoBehaviour
{
    Vector3 screenPos;
    Vector3 worldPos;
    [SerializeField] Transform refTransformMouseObject;
    public static Transform refTransformMouse;
    bool initialized;

    private void Start()
    {
        StartCoroutine(InitializeOnNextFrame());
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(worldPos, 0.5f);
    }

    void Update()
    {
        if (!initialized) return;

        screenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPos.z);
        
        worldPos = Camera.main.ScreenToWorldPoint(screenPos);
        refTransformMouse.position = worldPos;

        //print("SP (X:" + screenPos.x + " Y:" + screenPos.y + ") WP (X:" + worldPos.x + " Y:" + worldPos.y + " Z:" + worldPos.z + ")");
    }

    IEnumerator InitializeOnNextFrame()
    {
        yield return null;
        screenPos.z = Vector3.Distance(GameMaster.instance.playerObject.transform.position, Camera.main.transform.position);
        refTransformMouse = Instantiate(refTransformMouseObject, transform.position, Quaternion.identity);
        initialized = true;
        StopCoroutine(InitializeOnNextFrame());
    }

    private void OnDestroy()
    {
        if (refTransformMouse != null) refTransformMouse = null;
        initialized = false;
    }
}
