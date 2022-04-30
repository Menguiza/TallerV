using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PedradaMagica : MonoBehaviour, IHechizo
{
    public GameObject magicPebbleProyectile;

    float impulseForce = 8f;

    Transform attackPoint;

    float damage = 2f;
    public float Damage { get => damage; }

    private void Awake()
    {
        attackPoint = GameObject.Find("AttackPoint (1)").transform;
        magicPebbleProyectile = (GameObject)Resources.Load("Prefabs/Hechizos/MagicPebbleProyectile");
    }


    public void StartCastingSpell()
    {

    }

    public void CastSpell()
    {
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
        spellCastEvent.AddListener(CastSpell);
    }
}
