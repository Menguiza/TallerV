using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Babosa : MonoBehaviour, IEnemy
{
    public Transform player, attackPoint;

    public LayerMask whatIsPlayer;

    public float health, speed;

    float step;

    [SerializeField]
    int dmg = 1, conciencia = 1;
    [SerializeField]
    float force = 40, distance = 1f;

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
        original = transform.position;
        SearchWalkPoint();
    }

    private void Update()
    {
        if(health<=0)
        {
            return;
        }

        step = speed * Time.deltaTime;

        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if(playerInSightRange && !alreadyAttacked)
        {
            anim.SetBool("Moving", true);
        }
        else
        {
            anim.SetBool("Moving", false);
        }

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
        if (distanceToWalkPoint.magnitude <= 0.1f)
            walkPointSet = false;

        Turn();
    }

    private void SearchWalkPoint()
    {
        Vector3 distanceToWalkPoint = transform.position - original;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude <= 0.1f)
        {
            walkPoint = original;
            walkPoint.z = walkPoint.z + 6;
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

        float apart = Vector3.Distance(player.position, transform.position);

        walkPoint = player.position;
        walkPoint.y = 0;

        if (!alreadyAttacked && apart >= distance)
        {
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
        anim.SetTrigger("Die");
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        gameObject.GetComponent<Collider>().enabled = false;
        Destroy(gameObject, 1.6f);
    }

    public void ReceiveDamage(int dmg)
    {
        health -= dmg;

        if (health <= 0) DestroyEnemy();

        Vector3 moveDir = transform.position - player.position;

        gameObject.GetComponent<Rigidbody>().AddForce(moveDir.normalized * (float)force);
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
