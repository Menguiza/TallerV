using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BolaDeFuego : MonoBehaviour, IHechizo
{
    [SerializeField] GameObject fireball;
    Transform attackPoint;

    float impulseForce = 10f;

    //Esto habrá que cambiarlo luego
    private void Awake()
    {
        attackPoint = GameObject.Find("AttackPoint (1)").transform;
        fireball = (GameObject)Resources.Load("Prefabs/Hechizos/FireballProyectile");
    }

    public void StartCastingSpell()
    {

    }

    public void CastSpell()
    {
        print("Bola de fuego casteada");

        GameObject instance = Instantiate(fireball, attackPoint.position, Quaternion.identity);
        instance.GetComponent<Proyectil_BolaDeFuego>().damage = (int)GameMaster.instance.Player.Damage;
        instance.GetComponent<Rigidbody>().AddForce((attackPoint.forward + (Vector3.up/4)) * impulseForce, ForceMode.Impulse);

        //Particulas de lanzamiento acá
    }

    public void SubscribeToEvent(UnityEvent spellCastEvent)
    {
        spellCastEvent.AddListener(CastSpell);
    }
}
