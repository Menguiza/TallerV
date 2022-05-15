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

    float xDir;
    float yDir;
    float zDir;
    Vector3 direction;

    public void StartCastingSpell()
    {
        animator.SetTrigger("InstantCast Spell");

        PlayerController player = GameMaster.instance.playerObject.GetComponent<PlayerController>();
        player.SpellMethod = this;

        // El jugador mira a la derecha
        if (player.attackPoint.position.z > (player.transform.position.z))
        {
            if (!(SpellCastDirectionTracker.refTransformMouse.position.z > player.attackPoint2.position.z)) // Pero mira a la izquierda
            {
                player.TurnCharLeft();
            }
        }
        else // El jugador mira a la izquierda
        {
            if (!(SpellCastDirectionTracker.refTransformMouse.position.z < player.attackPoint2.position.z)) // Pero mira a la derecha
            {
                player.TurnCharRight();
            }
        }



        xDir = 0;
        yDir = SpellCastDirectionTracker.refTransformMouse.position.y - player.attackPoint2.position.y;
        zDir = SpellCastDirectionTracker.refTransformMouse.position.z - player.attackPoint2.position.z;
        direction = new Vector3(xDir, yDir, zDir).normalized;

        float minDistance = Vector3.Distance(player.attackPoint2.position, player.transform.position);
        float distanceFromPlayer = Vector3.Distance(SpellCastDirectionTracker.refTransformMouse.position, player.transform.position);

        if (distanceFromPlayer <= minDistance) direction = -direction;
    }

    public void CastSpell()
    {
        IsOnCD = true;
        remainingCD = CDTime;

        print("Flecha de fuego casteada");
        GameObject instance = Instantiate(fireArrowProyectile, attackPoint.position, Quaternion.identity);
        instance.GetComponent<Proyectil_FlechaDeFuego>().damage = damage;
        instance.GetComponent<Rigidbody>().AddForce(direction * impulseForce, ForceMode.Impulse);

        instance.transform.LookAt(direction * 5);

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
