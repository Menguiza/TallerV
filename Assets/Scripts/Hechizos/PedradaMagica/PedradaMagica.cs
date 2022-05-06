using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PedradaMagica : MonoBehaviour, IHechizo
{
    public GameObject magicPebbleProyectile;

    float impulseForce = 8f;

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

    Animator animator;

    private void Awake()
    {
        attackPoint = GameObject.Find("AttackPoint (1)").transform;
        magicPebbleProyectile = (GameObject)Resources.Load("Prefabs/Hechizos/MagicPebbleProyectile");
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

        //Proyectil real instanciado
        GameObject instance = Instantiate(magicPebbleProyectile, attackPoint.position, Quaternion.identity);
        instance.GetComponent<Proyectil_PedradaMagica>().damage = damage;
        instance.GetComponent<Rigidbody>().AddForce(((attackPoint.forward * 2) + transform.up).normalized * impulseForce, ForceMode.Impulse);

        instance.transform.LookAt(attackPoint.position + attackPoint.forward * 5);

        Destroy(instance, 3);

        //Particulas de lanzamiento acá 

        print("Pedrada mágica casteada");
    }

    public void SubscribeToEvent(UnityEvent spellCastEvent)
    {
        spellCastEvent.AddListener(StartCastingSpell);
    }
}
