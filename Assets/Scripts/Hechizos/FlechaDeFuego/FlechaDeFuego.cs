using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class FlechaDeFuego : MonoBehaviour, IHechizo
{
    public GameObject fireArrowProyectile;

    Transform attackPoint;

    float impulseForce = 30f;

    //Esto habr� que cambiarlo luego
    private void Awake()
    {
        attackPoint = GameObject.Find("AttackPoint (1)").transform;
        fireArrowProyectile = (GameObject)Resources.Load("Prefabs/Hechizos/FireArrowProyectile");
    }

    public void StartCastingSpell()
    {

    }

    public void CastSpell()
    {
        print("Flecha de fuego casteada");
        GameObject instance = Instantiate(fireArrowProyectile, attackPoint.position, Quaternion.identity);
        instance.GetComponent<Proyectil_FlechaDeFuego>().damage = (int)GameMaster.instance.Player.Damage;
        instance.GetComponent<Rigidbody>().AddForce((attackPoint.forward) * impulseForce, ForceMode.Impulse);

        Destroy(instance, 3);

        //Particulas de lanzamiento ac�
    }

    public void SubscribeToEvent(UnityEvent spellCastEvent)
    {
        spellCastEvent.AddListener(CastSpell);
    }
}
