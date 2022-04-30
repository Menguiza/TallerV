using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EspadaDeLuz : MonoBehaviour, IHechizo
{
    float damage = 1f;
    float buffDuration = 5f;
    public float Damage { get => damage; }

    public void StartCastingSpell()
    {

    }

    public void CastSpell()
    {
        sbyte totalValue = (sbyte)(10 * damage); // 10 equivale a +100%
        GameMaster.instance.AddMod("Espada de luz", 0,  totalValue, 0, 0, 0, 0, 0, 0, 0, 0, 0);
        Invoke(nameof(RemoveSpellEfect), buffDuration);

        print("Espada de luz casteado");
    }

    void RemoveSpellEfect()
    {
        GameMaster.instance.RemoveMod("Espada de luz");
    }

    public void SubscribeToEvent(UnityEvent spellCastEvent)
    {
        spellCastEvent.AddListener(CastSpell);
    }
}
