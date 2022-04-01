using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoomerangEnergia : MonoBehaviour, IHechizo
{
    public GameObject boomerangProyectile;

    Transform attackPoint;

    float impulseForce = 15f;

    //Esto habrá que cambiarlo luego
    private void Awake()
    {
        attackPoint = GameObject.Find("AttackPoint (1)").transform;
        boomerangProyectile = (GameObject)Resources.Load("Prefabs/Hechizos/EnergyBoomerang");
    }

    public void StartCastingSpell()
    {

    }

    public void CastSpell()
    {
        print("Boomerang de energia casteado");
        GameObject instance = Instantiate(boomerangProyectile, attackPoint.position, Quaternion.identity);
        instance.GetComponent<Proyectil_BoomerangEnergia>().damage = (int)GameMaster.instance.Player.Damage;
        instance.GetComponent<Rigidbody>().AddForce((attackPoint.forward) * impulseForce, ForceMode.Impulse);
        //Particulas de lanzamiento acá
    }

    public void SubscribeToEvent(UnityEvent spellCastEvent)
    {
        spellCastEvent.AddListener(CastSpell);
    }
}
