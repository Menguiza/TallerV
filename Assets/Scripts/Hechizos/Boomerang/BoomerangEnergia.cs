using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoomerangEnergia : MonoBehaviour, IHechizo
{
    public GameObject boomerangProyectile;

    Transform attackPoint;

    // IHechizo propiedades ---- >
    float damage;
    public float Damage { get => damage; set => damage = value; }

    float remainingCD;
    public float RemainingCD { get => remainingCD; set => remainingCD = value; }

    float CD_Time;
    public float CDTime { get => CD_Time; set => CD_Time = value; }

    bool isOnCD;
    public bool IsOnCD { get => isOnCD; set => isOnCD = value; }
    // < ----

    float impulseForce = 15f;

    Animator animator;

    //Esto habrá que cambiarlo luego
    private void Awake()
    {
        attackPoint = GameObject.Find("AttackPoint (1)").transform;
        boomerangProyectile = (GameObject)Resources.Load("Prefabs/Hechizos/EnergyBoomerang");
        animator = GameMaster.instance.playerObject.GetComponent<Animator>();
    }

    void Update()
    {
        if (remainingCD >= 0)
        {
            remainingCD -= Time.deltaTime;

            if (remainingCD < 0)
            {
                isOnCD = false;
            }
        }
    }

    public void StartCastingSpell()
    {
        animator.SetTrigger("InstantCast Spell");

        GameMaster.instance.playerObject.GetComponent<PlayerController>().SpellMethod = this;
    }

    public void CastSpell()
    {
        IsOnCD = true;
        remainingCD = CDTime;

        print("Boomerang de energia casteado");
        GameObject instance = Instantiate(boomerangProyectile, attackPoint.position, Quaternion.identity);
        instance.GetComponent<Proyectil_BoomerangEnergia>().damage = damage;
        instance.GetComponent<Rigidbody>().AddForce((attackPoint.forward) * impulseForce, ForceMode.Impulse);
        //Particulas de lanzamiento acá
    }

    public void SubscribeToEvent(UnityEvent spellCastEvent)
    {
        spellCastEvent.AddListener(StartCastingSpell);
    }
}
