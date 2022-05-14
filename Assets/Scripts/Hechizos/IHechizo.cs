using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IHechizo
{
    float Damage { get; }
    float RemainingCD { get; }
    float CDTime { get; }
    bool IsOnCD { get; }
    Animator AnimatorReference { get; }
    Transform AttackPointReference { get; }

    void StartCastingSpell();
    void CastSpell();
    void SubscribeToEvent(UnityEvent spellCastEvent);
    void SetVitalReferences();
}
