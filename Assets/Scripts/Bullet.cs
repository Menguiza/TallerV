using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Vector3 destination;
    float step;

    [SerializeField]
    float speed;
    [SerializeField]
    ParticleSystem particle;

    public LayerMask whatIsPlayer;

    VespulaFerus parent;

    bool hit = false;

    private void Awake()
    {
        destination = FindObjectOfType<PlayerController>().transform.position;
        destination.y = destination.y + 1f;
        parent = transform.GetComponentInParent<VespulaFerus>();
        particle.Stop();
    }

    public void Start()
    {
        transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, destination, step);
        transform.LookAt(destination);

        hit = Physics.CheckSphere(transform.position, 0.1f, whatIsPlayer);

        if(hit)
        {
            GameMaster.instance.DamagePlayer(parent.Damage, parent.Conciencia);
            particle.Play();
            particle.transform.parent = null;
            Destroy(particle.gameObject, 1f);
            Destroy(gameObject);
        }

        Vector3 distanceToWalkPoint = transform.position - destination;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude <= 0f)
        {
            particle.Play();
            particle.transform.parent = null;
            Destroy(particle.gameObject, 1f);
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.1f);
    }
}
