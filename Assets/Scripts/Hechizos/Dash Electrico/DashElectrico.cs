using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DashElectrico : MonoBehaviour, IHechizo
{
    float damage;
    public float Damage { get => damage; set => damage = value; }

    GameObject player;

    Animator animator;

    void Awake()
    {
        player = GameMaster.instance.playerObject;

        animator = GameMaster.instance.playerObject.GetComponent<Animator>();
    }


    public void StartCastingSpell()
    {
        animator.SetTrigger("DashCast Spell");

        GameMaster.instance.playerObject.GetComponent<PlayerController>().SpellMethod = this;
    }

    public void CastSpell()
    {
        player.gameObject.AddComponent<FuncionalidadDash>();
        player.gameObject.GetComponent<FuncionalidadDash>().damage = damage;

        print("Dash electrico casteado");
    }

    public void SubscribeToEvent(UnityEvent spellCastEvent)
    {
        spellCastEvent.AddListener(StartCastingSpell);
    }
}
