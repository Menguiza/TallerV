using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSceneTeleport : MonoBehaviour
{
    [SerializeField] Transform TPCentral, TPHechizos, TPPosturas;
    GameObject player;

    private void Start()
    {
        player = GameMaster.instance.playerObject;
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            player.transform.position = Vector3.Lerp(player.transform.position, TPPosturas.position, 1);
        }

        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            player.transform.position = Vector3.Lerp(player.transform.position, TPCentral.position, 1);
        }

        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            player.transform.position = Vector3.Lerp(player.transform.position, TPHechizos.position, 1);
        }
    }
}
