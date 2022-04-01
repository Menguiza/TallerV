using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class FlechaDeFuego : MonoBehaviour, IHechizo
{
    public void StartCastingSpell()
    {

    }

    public void CastSpell()
    {
        print("Flecha de fuego casteada");
    }

    public void SubscribeToEvent(UnityEvent spellCastEvent)
    {
        spellCastEvent.AddListener(CastSpell);
    }
}
