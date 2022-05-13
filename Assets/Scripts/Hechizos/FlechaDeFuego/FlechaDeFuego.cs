using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class FlechaDeFuego : MonoBehaviour, IHechizo
{
    GameObject fireArrowProyectile;
    float impulseForce = 30f;

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

    //Esto habrá que cambiarlo luego
    private void Awake()
    {
        attackPoint = GameObject.Find("AttackPoint (1)").transform;
        fireArrowProyectile = (GameObject)Resources.Load("Prefabs/Hechizos/FireArrowProyectile");
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
        animator.SetTrigger("InstantCast Spell");

        GameMaster.instance.playerObject.GetComponent<PlayerController>().SpellMethod = this;
    }

    public void CastSpell()
    {
        IsOnCD = true;
        remainingCD = CDTime;

        print("Flecha de fuego casteada");
        GameObject instance = Instantiate(fireArrowProyectile, attackPoint.position, Quaternion.identity);
        instance.GetComponent<Proyectil_FlechaDeFuego>().damage = damage;
        instance.GetComponent<Rigidbody>().AddForce((attackPoint.forward) * impulseForce, ForceMode.Impulse);

        instance.transform.LookAt(attackPoint.position + attackPoint.forward * 5);

        Destroy(instance, 3);

        //Particulas de lanzamiento acá
    }

    public void SubscribeToEvent(UnityEvent spellCastEvent)
    {
        spellCastEvent.AddListener(StartCastingSpell);
    }

    public void SetVitalReferences()
    {
        animator = GameMaster.instance.playerObject.GetComponent<Animator>();
        attackPoint = GameMaster.instance.playerObject.GetComponent<PlayerController>().attackPoint2;
    }
}
