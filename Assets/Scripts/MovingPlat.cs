using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlat : MonoBehaviour
{
    [SerializeField]
    Transform dest;

    [SerializeField]
    float distance, travelTime = 3;

    Vector3 tempPos, move;

    PlayerController player;
    Rigidbody rb;

    private void Awake()
    {
        dest.parent = null;
        tempPos = transform.position;
        player = FindObjectOfType<PlayerController>();
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        move = Vector3.Lerp(tempPos, dest.position,
            Mathf.Cos(Time.time / travelTime * Mathf.PI * 2) * -.5f + .5f);
        rb.MovePosition(move);

        if(player.feetHeight != null)
        {
            float dist = Vector3.Distance(player.feetHeight.position, transform.position);

            if (player.feetHeight.position.y > transform.position.y && dist <= distance)
            {
                player.GetComponent<CharacterController>().Move(rb.velocity * Time.deltaTime);
            }
        }
    }
}
