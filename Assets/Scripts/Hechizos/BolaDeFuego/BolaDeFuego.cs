using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BolaDeFuego : MonoBehaviour, IHechizo
{
    [SerializeField] GameObject fireball;
    Transform attackPoint;

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

    float impulseForce = 10f;

    Animator animator;

    //Esto habrá que cambiarlo luego
    private void Awake()
    {
        attackPoint = GameObject.Find("AttackPoint (1)").transform;
        fireball = (GameObject)Resources.Load("Prefabs/Hechizos/FireballProyectile");
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
        animator.SetTrigger("QuickCast Spell");

        GameMaster.instance.playerObject.GetComponent<PlayerController>().SpellMethod = this;
    }

    public void CastSpell()
    {
        IsOnCD = true;
        remainingCD = CDTime;

        print("Bola de fuego casteada");

        GameObject instance = Instantiate(fireball, attackPoint.position, Quaternion.identity);
        instance.GetComponent<Proyectil_BolaDeFuego>().damage = damage;
        instance.GetComponent<Rigidbody>().AddForce((attackPoint.forward) * impulseForce, ForceMode.Impulse);

        instance.transform.LookAt(attackPoint.position + attackPoint.forward * 5);

        Destroy(instance, 10);

        //Particulas de lanzamiento acá
    }

    public void SubscribeToEvent(UnityEvent spellCastEvent)
    {
        spellCastEvent.AddListener(StartCastingSpell);
    }
}
