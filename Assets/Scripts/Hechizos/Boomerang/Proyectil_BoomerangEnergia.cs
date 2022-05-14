using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil_BoomerangEnergia : MonoBehaviour
{
    public float damage;
    
    float returningForce = 15f;

    float minTimeBeforeReturn = 1.20f;
    float currentTimeSinceLaunch = 0f;

    Rigidbody rb;

    Transform returnTarget;
    bool hasBegunReturn;

    [SerializeField] GameObject boomerangTrail1;
    [SerializeField] GameObject boomerangTrail2;

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

            // VFX --- >
            boomerangTrail1.transform.SetParent(GameMaster.instance.playerObject.transform);
            boomerangTrail2.transform.SetParent(GameMaster.instance.playerObject.transform);

            boomerangTrail1.transform.localPosition = Vector3.up;
            boomerangTrail2.transform.localPosition = Vector3.up;

            var emmision = boomerangTrail1.GetComponent<ParticleSystem>().emission;
            emmision.enabled = false;
            emmision = boomerangTrail2.GetComponent<ParticleSystem>().emission;
            emmision.enabled = false;

            Destroy(boomerangTrail1, 2f);
            Destroy(boomerangTrail2, 2f);
            // < ---
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<IEnemy>() != null)
        {
            int spellDamage = GameMaster.instance.CalculateSpellDamage(damage);

            other.gameObject.GetComponent<IEnemy>().ReceiveDamage(spellDamage);
            GameObject popUpInstace = Instantiate(GameMaster.instance.DamagePopUp, other.transform.position + Vector3.up * 0.5f + Vector3.right, GameMaster.instance.DamagePopUp.transform.rotation);
            popUpInstace.GetComponent<DamagePopUp>().SetText(AttackType.normal, spellDamage);
        }
    }
}
