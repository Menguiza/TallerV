using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BolaDeAcido : MonoBehaviour, IHechizo
{
    public GameObject acidBallProyectile;

    float impulseForce = 15f;

    Transform attackPoint;

    float damage = 2f;
    public float Damage { get => damage; }

    Animator animator;

    private void Awake()
    {
        attackPoint = GameObject.Find("AttackPoint (1)").transform;
        acidBallProyectile = (GameObject)Resources.Load("Prefabs/Hechizos/AcidBallProyectile");
        animator = GameMaster.instance.playerObject.GetComponent<Animator>();
    }


    public void StartCastingSpell()
    {
        animator.SetTrigger("QuickCast Spell");

        GameMaster.instance.playerObject.GetComponent<PlayerController>().SpellMethod = this;
    }

    public void CastSpell()
    {
        //Proyectil real instanciado
        GameObject instance = Instantiate(acidBallProyectile, attackPoint.position, Quaternion.identity);
        instance.GetComponent<Proyectil_BolaDeAcido>().damage = damage;
        instance.GetComponent<Rigidbody>().AddForce((attackPoint.forward) * impulseForce, ForceMode.Impulse);

        instance.transform.LookAt(attackPoint.position + attackPoint.forward * 5);

        Destroy(instance, 3);

        //Particulas de lanzamiento acá 

        print("Bola de ácido casteada");
    }

    public void SubscribeToEvent(UnityEvent spellCastEvent)
    {
        spellCastEvent.AddListener(StartCastingSpell);
    }
}
