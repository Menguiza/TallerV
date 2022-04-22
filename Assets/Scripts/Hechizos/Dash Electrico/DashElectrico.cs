using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DashElectrico : MonoBehaviour, IHechizo
{
    float damage = 1.5f;
    public float Damage { get => damage; }

    GameObject player;

    void Awake()
    {
        player = GameMaster.instance.playerObject;
    }


    public void StartCastingSpell()
    {

    }

    public void CastSpell()
    {
        player.gameObject.AddComponent<FuncionalidadDash>();
        player.gameObject.GetComponent<FuncionalidadDash>().damage = damage;

        print("Dash electrico casteado");
    }

    public void SubscribeToEvent(UnityEvent spellCastEvent)
    {
        spellCastEvent.AddListener(CastSpell);
    }
}
