using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PedradaMagica : MonoBehaviour, IHechizo
{
    float damage = 3.5f;
    public float Damage { get => damage; }

    public void StartCastingSpell()
    {

    }

    public void CastSpell()
    {
        print("Pedrada mágica casteada");
    }

    public void SubscribeToEvent(UnityEvent spellCastEvent)
    {
        spellCastEvent.AddListener(CastSpell);
    }
}
