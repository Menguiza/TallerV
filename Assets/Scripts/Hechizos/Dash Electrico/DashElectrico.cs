using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DashElectrico : MonoBehaviour, IHechizo
{
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

    GameObject player;

    Animator animator;

    void Awake()
    {
        player = GameMaster.instance.playerObject;

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
        animator.SetTrigger("DashCast Spell");

        GameMaster.instance.playerObject.GetComponent<PlayerController>().SpellMethod = this;
    }

    public void CastSpell()
    {
        IsOnCD = true;
        remainingCD = CDTime;

        player.gameObject.AddComponent<FuncionalidadDash>();
        player.gameObject.GetComponent<FuncionalidadDash>().damage = damage;

        print("Dash electrico casteado");
    }

    public void SubscribeToEvent(UnityEvent spellCastEvent)
    {
        spellCastEvent.AddListener(StartCastingSpell);
    }
}
