using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IHechizo
{
    void StartCastingSpell();
    void CastSpell();
    void SubscribeToEvent(UnityEvent spellCastEvent);
}