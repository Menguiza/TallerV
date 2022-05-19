using UnityEngine;

public class VespulaFerus : MonoBehaviour, IEnemy
{
    public Transform player, parent;

    public LayerMask whatIsPlayer, whatsMe;

    public float health, speed;

    float step;

    [SerializeField]
    int dmg = 1, conciencia = 1;

    public int Damage { get => dmg; set => dmg = value; }
    public int Conciencia { get => conciencia; set => conciencia = value; }

    //Patroling
    public Vector3 walkPoint, fixedPivot, origin, collision;
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
        origin = parent.position;
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

        if(playerInSightRange)
        {
            transform.LookAt(player);
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
        if(transform.position.z == origin.z)
        {
            walkPoint = new Vector3(transform.position.x, transform.position.y, transform.position.z + walkPointRange);
        }
        else
        {
            walkPoint = origin;
        }
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, step);
    }

    
    private void AttackPlayer()
    {
        collision = transform.position;
        collision.y = player.position.y + 0.6f;

        //Make sure enemy doesn't move
        transform.position = Vector3.MoveTowards(transform.position, collision, step);

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

    public void DestroyEnemy()
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

    [SerializeField] GameObject GetHitParticle;

    public void ReceiveDamage(int dmg)
    {
        health -= dmg;

        //SFX
        GameObject instanceParticle = Instantiate(GetHitParticle, transform.position + Vector3.up, Quaternion.identity);
        Destroy(instanceParticle, 4f);

        if (health <= 0) DestroyEnemy();
    }
}
