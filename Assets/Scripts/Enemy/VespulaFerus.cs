using UnityEngine;
using UnityEngine.AI;

public class VespulaFerus : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player, parent, attackPoint;

    public LayerMask whatIsGround, whatIsPlayer;

    public float health, offset, attackRad;

    public int dmg = 1;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange, playerhited;

    Vector3 vectorFixed;
    public Animator anim;

    private void Awake()
    {
        player = GameObject.Find("KK").transform;
        agent = GetComponent<NavMeshAgent>();
        parent = transform.parent;
        agent.updateRotation = false;
    }

    private void Update()
    {
        vectorFixed = new Vector3(transform.position.x, transform.position.y + offset, transform.position.z);

        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(vectorFixed, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(vectorFixed, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange)
        {
            transform.rotation = Quaternion.LookRotation(agent.velocity.normalized);
            Patroling();
        }

        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }

    private void Patroling()
    {
        agent.speed = 1;

        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        if(transform.position.z == parent.position.z)
        {
            walkPoint = new Vector3(transform.position.x, transform.position.y, transform.position.z + walkPointRange);
        }
        else
        {
            walkPoint = parent.position;
        }

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        if(!alreadyAttacked)
        {
            agent.speed = 3.5f;
            agent.SetDestination(player.position);
        }
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            ///Attack code here
            anim.SetTrigger("Attack");
            playerhited = Physics.CheckSphere(attackPoint.position, attackRad, whatIsPlayer);

            if(playerhited)
            {
                GameMaster.instance.DamagePlayer(dmg);
            }

            ///End of attack code
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        anim.ResetTrigger("Attack");
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }
    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(vectorFixed, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(vectorFixed, sightRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(attackPoint.position, attackRad);
    }
}
