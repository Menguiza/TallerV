using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Babosa : MonoBehaviour, IEnemy
{
    public Transform player, attackPoint, destiny;

    public LayerMask whatIsPlayer;

    public float health, speed;

    float step;

    [SerializeField]
    int dmg = 1, conciencia = 1;

    public int Damage { get => dmg; set => dmg = value; }
    public int Conciencia { get => conciencia; set => conciencia = value; }

    //Patroling
    public Vector3 walkPoint, original;
    bool walkPointSet;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange, attackRad;
    public bool playerInSightRange, playerInAttackRange, playerHitted;

    public Animator anim;

    Quaternion rotation;

    private void Awake()
    {
        player = GameObject.FindObjectOfType<PlayerController>().transform;
        destiny.parent = null;
        original = transform.position;
    }

    private void Update()
    {
        step = speed * Time.deltaTime;

        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        anim.SetBool("Moving", !playerInAttackRange);

        if (!playerInSightRange && !playerInAttackRange)
        {
            Patroling();
        }

        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            transform.position = Vector3.MoveTowards(transform.position, walkPoint, step);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude <= 0f)
            walkPointSet = false;

        Turn();
    }

    private void SearchWalkPoint()
    {
        if(transform.position == original)
        {
            walkPoint = destiny.position;
        }
        else
        {
            walkPoint = original;
        }

        walkPointSet = true;
    }

    private void ChasePlayer()
    {
        Turn();

        if (!alreadyAttacked)
        {
            walkPoint = player.position;
            walkPoint.y = 0;

            transform.position = Vector3.MoveTowards(transform.position, walkPoint, step);
        }
    }


    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        transform.position = Vector3.MoveTowards(transform.position, transform.position, step);

        Turn();

        if (!alreadyAttacked)
        {
            ///Attack code here
            anim.SetTrigger("Attack");

            ///End of attack code
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }


    private void ResetAttack()
    {
        anim.ResetTrigger("Attack");
        alreadyAttacked = false;
        playerHitted = false;
    }

    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    public void ReceiveDamage(int dmg)
    {
        health -= dmg;

        if (health <= 0) DestroyEnemy();

        if (transform.position.z > player.position.z)
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(0, 2, 3), ForceMode.Impulse);
        }
        else
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(0, 2, -3), ForceMode.Impulse);
        }
    }

    void Turn()
    {
        if (transform.position.z > walkPoint.z)
        {
            rotation = Quaternion.Euler(0, 180, 0);
            transform.rotation = rotation;
        }
        else
        {
            rotation = Quaternion.Euler(0, 0, 0);
            transform.rotation = rotation;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(attackPoint.position, attackRad);
    }

    public void Hit()
    {
        playerHitted = Physics.CheckSphere(attackPoint.position, attackRad, whatIsPlayer);

        if (playerHitted)
        {
            GameMaster.instance.DamagePlayer(Damage, conciencia);
        }
    }

}
