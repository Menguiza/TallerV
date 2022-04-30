using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BolaDeFuego : MonoBehaviour, IHechizo
{
    [SerializeField] GameObject fireball;
    Transform attackPoint;

    float damage = 3f;
    public float Damage { get => damage; }

    float impulseForce = 10f;

    Animator animator;

    //Esto habr� que cambiarlo luego
    private void Awake()
    {
        attackPoint = GameObject.Find("AttackPoint (1)").transform;
        fireball = (GameObject)Resources.Load("Prefabs/Hechizos/FireballProyectile");
        animator = GameMaster.instance.playerObject.GetComponent<Animator>();
    }

    public void StartCastingSpell()
    {
        animator.SetTrigger("QuickCast Spell");

        GameMaster.instance.playerObject.GetComponent<PlayerController>().SpellMethod = this;
    }

    public void CastSpell()
    {
        print("Bola de fuego casteada");

        GameObject instance = Instantiate(fireball, attackPoint.position, Quaternion.identity);
        instance.GetComponent<Proyectil_BolaDeFuego>().damage = damage;
        instance.GetComponent<Rigidbody>().AddForce((attackPoint.forward) * impulseForce, ForceMode.Impulse);

        instance.transform.LookAt(attackPoint.position + attackPoint.forward * 5);

        Destroy(instance, 10);

        //Particulas de lanzamiento ac�
    }

    public void SubscribeToEvent(UnityEvent spellCastEvent)
    {
        spellCastEvent.AddListener(StartCastingSpell);
    }
}
