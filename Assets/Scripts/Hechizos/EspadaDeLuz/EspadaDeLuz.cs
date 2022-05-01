using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EspadaDeLuz : MonoBehaviour, IHechizo
{
    float damage;
    float buffDuration = 5f;
    public float Damage { get => damage; set => damage = value; }

    Animator animator;

    private void Awake()
    {
        animator = GameMaster.instance.playerObject.GetComponent<Animator>();
    }

    public void StartCastingSpell()
    {
        animator.SetTrigger("BuffCast Spell");

        GameMaster.instance.playerObject.GetComponent<PlayerController>().SpellMethod = this;
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
        spellCastEvent.AddListener(StartCastingSpell);
    }
}
