using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoomerangEnergia : MonoBehaviour, IHechizo
{
    public void StartCastingSpell()
    {

    }

    public void CastSpell()
    {
        print("Boomerang de energia casteado");
    }

    public void SubscribeToEvent(UnityEvent spellCastEvent)
    {
        spellCastEvent.AddListener(CastSpell);
    }
}
