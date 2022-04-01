using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil_BoomerangEnergia : MonoBehaviour
{
    public int damage;
    float returningForce = 15f;

    float minTimeBeforeReturn = 1.20f;
    float currentTimeSinceLaunch = 0f;

    Rigidbody rb;

    Transform returnTarget;
    bool hasBegunReturn;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        returnTarget = GameObject.Find("AttackPoint (1)").transform;
    }

    private void FixedUpdate()
    {
        currentTimeSinceLaunch += Time.fixedDeltaTime;

        if (currentTimeSinceLaunch > minTimeBeforeReturn)
        {
            hasBegunReturn = true;
        }

        if (!hasBegunReturn)
        {
            rb.AddForce((returnTarget.position - transform.position).normalized * returningForce);
        }
        else
        {
            rb.velocity = Vector3.zero;
            rb.AddForce((returnTarget.position - transform.position).normalized * returningForce, ForceMode.VelocityChange);
            
        }

        //Rotar
        Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, 800, 0) * Time.fixedDeltaTime);
        rb.MoveRotation(rb.rotation * deltaRotation);
       
        if (Vector3.Distance(transform.position, returnTarget.position) <= 1.5f && currentTimeSinceLaunch > minTimeBeforeReturn)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<EnemyController>() != null)
        {
            other.gameObject.GetComponent<EnemyController>().Life = (uint)Mathf.Max(0, other.gameObject.GetComponent<EnemyController>().Life - damage);
        }
    }
}
