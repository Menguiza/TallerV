using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour, IEnemy
{
	public Transform player, attackPoint;
	public LayerMask playerLayer;

	float health = 200;

	public float altitude { get; private set; }

	public bool isFlipped = false;

	[SerializeField]
	int dmg, conciencia;

	public int Damage { get => dmg; set => dmg = value; }
	public int Conciencia { get => conciencia; set => conciencia = value; }

	[SerializeField] GameObject prefab, bullet;

	private void Start()
	{
		player = GameMaster.instance.playerObject.transform;
		altitude = transform.position.y;
	}

	[SerializeField] GameObject GetHitParticle;

	public void DestroyEnemy()
	{
		Destroy(gameObject);
	}

	public void ReceiveDamage(int dmg)
	{
		health -= dmg;

		//SFX
		GameObject instanceParticle = Instantiate(GetHitParticle, transform.position + Vector3.up, Quaternion.identity);
		Destroy(instanceParticle, 4f);

		if (health <= 0) DestroyEnemy();
	}

	public void LookAtPlayer()
	{
		if(player != null)
        {
			if (transform.position.z > player.position.z && isFlipped)
			{
				transform.Rotate(0f, 180f, 0f);
				isFlipped = false;
			}
			else if (transform.position.z < player.position.z && !isFlipped)
			{
				transform.Rotate(0f, 180f, 0f);
				isFlipped = true;
			}
		}
	}

	public void Spawn()
	{
		GameObject abeja = Instantiate(prefab, transform.position, Quaternion.identity, transform);
		abeja.transform.parent = null;
	}

	public bool CollisionDetection()
	{
		bool nada = false;
		Collider[] hitPlayer = Physics.OverlapSphere(attackPoint.position, 1, playerLayer);

		foreach (Collider player in hitPlayer)
		{
			if (player.GetComponent<PlayerController>() == null) continue;

			player.GetComponent<PlayerController>().Embestido(this.gameObject);
			nada = true;

			return nada;
		}

		return nada;
	}

	public void Attack()
    {
		Instantiate(bullet, transform.position, Quaternion.identity, transform);
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(attackPoint.position, 1);
	}
}
