using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BolaDeAcido : MonoBehaviour, IHechizo
{
    public void StartCastingSpell()
    {

    }

    public void CastSpell()
    {
        print("Bola de ácido casteada");
    }

    public void SubscribeToEvent(UnityEvent spellCastEvent)
    {
        spellCastEvent.AddListener(CastSpell);
    }
}
