using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EspadaDeLuz : MonoBehaviour, IHechizo
{
    public void StartCastingSpell()
    {

    }

    public void CastSpell()
    {
        print("Espada de luz casteado");
    }

    public void SubscribeToEvent(UnityEvent spellCastEvent)
    {
        spellCastEvent.AddListener(CastSpell);
    }
}
