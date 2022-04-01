using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BolaDeFuego : MonoBehaviour, IHechizo
{
    [SerializeField] GameObject fireball;

    public void StartCastingSpell()
    {

    }

    public void CastSpell()
    {
        print("Bola de fuego casteada");
    }

    public void SubscribeToEvent(UnityEvent spellCastEvent)
    {
        spellCastEvent.AddListener(CastSpell);
    }
}
