using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EspadaDeLuz : MonoBehaviour, IHechizo
{
    // IHechizo propiedades ---- >
    float damage;
    public float Damage { get => damage; set => damage = value; }

    float remainingCD;
    public float RemainingCD { get => remainingCD; set => remainingCD = value; }

    float CD_Time;
    public float CDTime { get => CD_Time; set => CD_Time = value; }

    bool isOnCD;
    public bool IsOnCD { get => isOnCD; set => isOnCD = value; }
    // < ----

    float buffDuration = 5f;

    Animator animator;

    GameObject[] VFX_Espada; // Implementar por medio de ResourcesLoad o por Activación de la particula previamente puesta en Escena

    private void Awake()
    {
        animator = GameMaster.instance.playerObject.GetComponent<Animator>();
    }

    void Update()
    {
        if (remainingCD >= 0)
        {
            remainingCD -= Time.deltaTime;

            if (remainingCD < 0)
            {
                isOnCD = false;
            }
        }
    }

    public void StartCastingSpell()
    {
        animator.SetTrigger("BuffCast Spell");

        GameMaster.instance.playerObject.GetComponent<PlayerController>().SpellMethod = this;
    }

    public void CastSpell()
    {
        IsOnCD = true;
        remainingCD = CDTime;

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
