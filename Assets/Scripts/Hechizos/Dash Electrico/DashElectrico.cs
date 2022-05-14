using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DashElectrico : MonoBehaviour, IHechizo
{
    GameObject player;
    GameObject TrailVFX;
    GameObject trail;

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

    Transform attackPoint;
    public Transform AttackPointReference { get => attackPoint; set => attackPoint = value; }
    // < ----

    void Awake()
    {
        player = GameMaster.instance.playerObject;
        animator = GameMaster.instance.playerObject.GetComponent<Animator>();
        TrailVFX = (GameObject)Resources.Load("Prefabs/VFX_Hechizos/VFX_ElectricDash_Trail");
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
        animator.SetTrigger("DashCast Spell");

        GameMaster.instance.playerObject.GetComponent<PlayerController>().SpellMethod = this;
    }

    public void CastSpell()
    {
        IsOnCD = true;
        remainingCD = CDTime;

        player.gameObject.AddComponent<FuncionalidadDash>();
        player.gameObject.GetComponent<FuncionalidadDash>().damage = damage;

        trail = Instantiate(TrailVFX, player.transform.position + Vector3.up, Quaternion.identity, player.transform);
        Invoke(nameof(DeactiveTrailEmission), 0.1f);
        Destroy(trail, 3f);

        print("Dash electrico casteado");
    }

    public void SubscribeToEvent(UnityEvent spellCastEvent)
    {
        spellCastEvent.AddListener(StartCastingSpell);
    }

    void DeactiveTrailEmission()
    {
        var emmision = trail.transform.GetChild(0).GetChild(0).GetComponent<ParticleSystem>().emission;
        emmision.enabled = false;
    }

    public void SetVitalReferences()
    {
        animator = GameMaster.instance.playerObject.GetComponent<Animator>();
        attackPoint = GameMaster.instance.playerObject.GetComponent<PlayerController>().attackPoint2;
        player = GameMaster.instance.playerObject;
    }
}
