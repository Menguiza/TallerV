using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EspadaDeLuz : MonoBehaviour, IHechizo
{
    float buffDuration = 5f;

    // IHechizo propiedades ---- >
    float damage;
    public float Damage { get => damage; set => damage = value; }

    float remainingCD;
    public float RemainingCD { get => remainingCD; set => remainingCD = value; }

    float CD_Time;
    public float CDTime { get => CD_Time; set => CD_Time = value; }

    bool isOnCD;
    public bool IsOnCD { get => isOnCD; set => isOnCD = value; }

    Animator animator;
    public Animator AnimatorReference { get => animator; set => animator = value; }

    Transform attackPoint; // No se necesita su uso aquí, sin embargo lo implementa por ser IHechizo
    public Transform AttackPointReference { get => attackPoint; set => attackPoint = value; }
    // < ----

    private void Awake()
    {
        animator = GameMaster.instance.playerObject.GetComponent<Animator>();
    }

    void Update()
    {
        if (remainingCD >= 0)
        {
            remainingCD -= Time.deltaTime * ManagerHechizos.instance.spellCastSpeedMultiplier;

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
        Invoke(nameof(StopEmittingParticles), 4f);

        GameMaster.instance.playerObject.GetComponent<PlayerController>().espadaDeLuz.GetComponent<MeshRenderer>().enabled = true;
        GameMaster.instance.playerObject.GetComponent<PlayerController>().espadaDeLuz.transform.GetChild(2).gameObject.GetComponent<ParticleSystem>().Play();
        GameMaster.instance.playerObject.GetComponent<PlayerController>().espadaDeLuz.transform.GetChild(3).gameObject.GetComponent<ParticleSystem>().Play();

        print("Espada de luz casteado");
    }

    void StopEmittingParticles()
    {
        GameMaster.instance.playerObject.GetComponent<PlayerController>().espadaDeLuz.transform.GetChild(2).gameObject.GetComponent<ParticleSystem>().Stop();
        GameMaster.instance.playerObject.GetComponent<PlayerController>().espadaDeLuz.transform.GetChild(3).gameObject.GetComponent<ParticleSystem>().Stop();
    }

    void RemoveSpellEfect()
    {
        GameMaster.instance.playerObject.GetComponent<PlayerController>().espadaDeLuz.GetComponent<MeshRenderer>().enabled = false;
        GameMaster.instance.RemoveMod("Espada de luz");
    }

    public void SubscribeToEvent(UnityEvent spellCastEvent)
    {
        spellCastEvent.AddListener(StartCastingSpell);
    }

    public void SetVitalReferences()
    {
        animator = GameMaster.instance.playerObject.GetComponent<Animator>();
    }
}
