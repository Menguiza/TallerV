using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoomerangEnergia : MonoBehaviour, IHechizo
{
    public GameObject boomerangProyectile;

    Transform attackPoint;

    float damage;
    public float Damage { get => damage; set => damage = value; }

    float impulseForce = 15f;

    Animator animator;

    //Esto habrá que cambiarlo luego
    private void Awake()
    {
        attackPoint = GameObject.Find("AttackPoint (1)").transform;
        boomerangProyectile = (GameObject)Resources.Load("Prefabs/Hechizos/EnergyBoomerang");
        animator = GameMaster.instance.playerObject.GetComponent<Animator>();
    }

    public void StartCastingSpell()
    {
        animator.SetTrigger("InstantCast Spell");

        GameMaster.instance.playerObject.GetComponent<PlayerController>().SpellMethod = this;
    }

    public void CastSpell()
    {
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
