using UnityEngine;

public class VespulaFerus : MonoBehaviour
{
    public Transform player, parent;

    public LayerMask whatIsPlayer;

    public float health, speed;

    float step;

    public int dmg = 1;

    //Patroling
    public Vector3 walkPoint, fixedPivot;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    public Animator anim;

    public GameObject bullet;

    Quaternion rotation;

    private void Awake()
    {
        player = GameObject.FindObjectOfType<PlayerController>().transform;
        parent = transform.parent;
    }

    private void Update()
    {
        step = speed * Time.deltaTime;

        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

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
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;

        if(transform.position.z > walkPoint.z)
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
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, step);
        }
    }

    
    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        transform.position = Vector3.MoveTowards(transform.position, transform.position, step);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            ///Attack code here
            anim.SetTrigger("Attack");

            fixedPivot = transform.position;
            fixedPivot.y = fixedPivot.y + 0.6f;

            Instantiate(bullet, fixedPivot, Quaternion.identity, transform);

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
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
