using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 3;
    [SerializeField]
    Transform attackPoint, attackPoint2;
    [SerializeField]
    LayerMask enemyLayer;
    [SerializeField]
    GameObject particles, particlesChild;

    CharacterController characterContrl;
    Animator anim;

    GameMaster gm;

    bool airAttack = false, dodge = false, knockBacked = false, died = false;
    public bool blocking = false, dodgeEnable = true, attack = false;

    //Variables Utilidad
    byte hundred = 100, zero = 0, fullTurn = 180;

    [SerializeField]
    private float jumpFoce = 10f, dodgeForce = 100f, knockBackForce = 5f, delay = 3f;
    public float resetDodgeTime = 2f;

    [SerializeField]
    float gravity = 9.8f, attackRange = 1f;
    float horizontal, verticalVelocity;

    Vector3 move = Vector3.zero;

    private void Start()
    {
        characterContrl = gameObject.GetComponent<CharacterController>();
        anim = gameObject.GetComponent<Animator>();
        gm = FindObjectOfType<GameMaster>();

        gm.playerObject = this.gameObject;
        gm.particles = this.particles;
        gm.particlesChild = this.particlesChild;

        particles.GetComponent<ParticleSystem>().Stop();
        particlesChild.GetComponent<ParticleSystem>().Stop();

        gm.sceneReloaded = false; //Resetear sceneReloaded para permitir la recarga de subsecuentes escenas
    }


    void Update()
    {
        Move();

        if (!died)
        {
            Attack();
        }

        if (gm.Player.Life <= zero && !died)
        {
            ResetAnimDodge();
            ResetAnimDodge2();
            InactiveCollider();
            InactiveCollider2();
            died = true;
            anim.SetTrigger("Died");
            Destroy(gameObject, delay);
        }

        anim.SetFloat("VelocidadAtaque", gm.Player.MultVelAtaque);
    }

    private void FixedUpdate()
    {
        if ((characterContrl.collisionFlags & CollisionFlags.Above) != 0)
        {
            if(verticalVelocity > zero)
            {
                verticalVelocity = zero;
            }
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!died)
        {
            if (hit.collider.CompareTag("Enemy") && !anim.GetBool("Knocked"))
            {
                anim.SetBool("Jump", false);
                anim.SetTrigger("Knock");
                anim.SetBool("Knocked", true);
                move = -transform.forward * knockBackForce;
                knockBacked = true;
                ResetAnimDodge();
                ResetAnimDodge2();
                InactiveCollider();
                InactiveCollider2();
                gm.DamagePlayer((int)hit.collider.GetComponent<EnemyController>().conciencia);
            }
            else if (hit.collider.GetComponent<TrapContainer>() != null && !anim.GetBool("Knocked"))
            {
                anim.SetBool("Jump", false);
                anim.SetTrigger("Knock");
                anim.SetBool("Knocked", true);
                move = -transform.forward * knockBackForce;
                knockBacked = true;
                ResetAnimDodge();
                ResetAnimDodge2();
                InactiveCollider();
                InactiveCollider2();
                gm.DamagePlayer((int)hit.collider.GetComponent<TrapContainer>().trap.damage);
            }
        }
    }

    #region "Movimiento"

    private void Attack()
    {
        if (Input.GetButtonDown("Punch") && characterContrl.isGrounded && !attack && !dodge && !knockBacked && !blocking && !airAttack)
        {
            anim.SetTrigger("Punch");

            attack = true;

            anim.SetFloat("Horizontal", zero);
        }
        else if (Input.GetButtonDown("Punch") && !characterContrl.isGrounded && !dodge && !knockBacked && !airAttack && !attack)
        {
            airAttack = true;
            anim.SetTrigger("AirAttack");
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }

        Gizmos.DrawSphere(attackPoint.position, attackRange);
    }

    private void Move()
    {
        if (attack && !knockBacked || died)
        {
            horizontal = zero;

            move.z = horizontal;

            anim.SetFloat("Horizontal", zero);
        }
        else if (!characterContrl.isGrounded && !knockBacked && !blocking && !died && !anim.GetBool("Knocked"))
        {
            if (Input.GetAxisRaw("Horizontal") != zero)
            {
                horizontal = Input.GetAxisRaw("Horizontal");
            }

            if (Input.GetButtonDown("Dodge") && !dodge && horizontal != zero && !attack && dodgeEnable)
            {
                dodge = true;
                dodgeEnable = false;
                anim.SetTrigger("DodgeAir");

                if (Input.GetAxisRaw("Horizontal") != zero && Input.GetAxisRaw("Horizontal") != horizontal)
                {
                    horizontal = Input.GetAxisRaw("Horizontal");
                }

                if (horizontal < zero)
                {
                    move.z -= dodgeForce * Time.deltaTime;
                }
                else if (horizontal > zero)
                {
                    move.z += dodgeForce * Time.deltaTime;
                }
            }
            else if (!dodge && horizontal != zero && anim.GetBool("Jump"))
            {
                TurnChar();
            }
            else if (dodge)
            {
                TurnCharDash();
            }
        }
        else if (!knockBacked && !died && !anim.GetBool("Knocked"))
        {
            if (Input.GetButtonDown("Dodge") && dodgeEnable && Input.GetAxisRaw("Horizontal") != zero && !knockBacked && !blocking && !attack)
            {
                dodge = true;
                dodgeEnable = false;
                anim.SetTrigger("DodgeGround");
                horizontal = Input.GetAxisRaw("Horizontal");

                if (horizontal < zero)
                {
                    move.z -= dodgeForce * Time.deltaTime;
                }
                else if (horizontal > zero)
                {
                    move.z += dodgeForce * Time.deltaTime;
                }
            }
            else
            {
                if (Input.GetButton("Block") && !knockBacked && !dodge)
                {
                    blocking = true;
                    anim.SetBool("Block", true);
                }
                else if (!dodge)
                {
                    horizontal = Input.GetAxisRaw("Horizontal");
                    anim.SetFloat("Horizontal", Input.GetAxis("Horizontal"));
                    TurnChar();
                    blocking = false;
                    anim.SetBool("Block", false);
                }
                else if (dodge)
                {
                    TurnCharDash();
                }
            }
        }

        SetGravity();

        if (Input.GetButtonDown("Jump") && characterContrl.isGrounded && !attack && !dodge && !knockBacked && !died)
        {
            verticalVelocity = jumpFoce;
            anim.SetBool("Jump", true);
            move.y = verticalVelocity;
        }
        else if (knockBacked && characterContrl.isGrounded && !died)
        {
            verticalVelocity = jumpFoce;
            move.y = verticalVelocity;
        }

        if (characterContrl.isGrounded && verticalVelocity < zero)
        {
            anim.SetBool("Knocked", false);
            anim.SetBool("Jump", false);
        }

        characterContrl.Move(move * Time.deltaTime);
    }

    void SetGravity()
    {
        if (characterContrl.isGrounded)
        {
            verticalVelocity = -gravity * Time.deltaTime;
            move.y = verticalVelocity;
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
            move.y = verticalVelocity;
        }
    }

    void TurnChar()
    {
        if (horizontal < zero)
        {
            transform.rotation = new Quaternion(zero, -fullTurn, zero, zero);
            move = -transform.forward * horizontal * (speed * gm.Player.SpeedMult);
        }
        else if (horizontal > zero)
        {
            transform.rotation = new Quaternion(zero, zero, zero, zero);
            move = transform.forward * horizontal * (speed * gm.Player.SpeedMult);
        }
        else
        {
            move.z = zero;
        }
    }

    void TurnCharDash()
    {
        if (horizontal < zero)
        {
            move.z -= dodgeForce * Time.deltaTime;
        }
        else if (horizontal > zero)
        {
            move.z += dodgeForce * Time.deltaTime;
        }
        else
        {
            move.z = zero;
        }
    }

    #endregion

    #region"Metodos de Utilidad"
    public void ActiveCollider()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider enemy in hitEnemies)
        {
            uint dañoAplicar = ProbabilidadCritico(gm.Player);
            uint result = (uint)Mathf.Max(0, enemy.gameObject.GetComponent<EnemyController>().Life - dañoAplicar);
            enemy.gameObject.GetComponent<EnemyController>().Life = result;
            RoboDeVida(dañoAplicar);

            gm.enableTGPC = false;

            if (gm.Player.Status == GameMaster.estado.Dormido && gm.Player.Conciencia < gm.Player.MaxConciencia)
            {
                gm.Player.Conciencia -= (ushort)enemy.gameObject.GetComponent<EnemyController>().conciencia;
            }
        }
    }

    public void ActiveCollider2()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint2.position, attackRange, enemyLayer);

        foreach (Collider enemy in hitEnemies)
        {
            uint dañoAplicar = ProbabilidadCritico(gm.Player);
            uint result = (uint)Mathf.Max(0, enemy.gameObject.GetComponent<EnemyController>().Life - dañoAplicar);
            enemy.gameObject.GetComponent<EnemyController>().Life = result;
            RoboDeVida(dañoAplicar);

            gm.enableTGPC = false;

            if (gm.Player.Status == GameMaster.estado.Dormido && gm.Player.Conciencia < gm.Player.MaxConciencia)
            {
                gm.Player.Conciencia -= (ushort)enemy.gameObject.GetComponent<EnemyController>().conciencia;
            }
        }
    }

    public void InactiveCollider()
    {
        attack = false;
        anim.ResetTrigger("Punch");
        Invoke("ResetTGPC", gm.timeToReset);
    }

    public void InactiveCollider2()
    {
        airAttack = false;
        anim.ResetTrigger("AirAttack");
    }

    private void ResetTGPC()
    {
        gm.enableTGPC = true;
    }

    public void Dodge()
    {
        Invoke("ResetDodge", resetDodgeTime);
    }

    private void ResetDodge()
    {
        dodgeEnable = true;
    }

    public void ResetAnimDodge()
    {
        anim.ResetTrigger("DodgeAir");
        dodge = false;
    }
    public void ResetAnimDodge2()
    {
        anim.ResetTrigger("DodgeGround");
        dodge = false;
    }

    public void ResetKnockBack()
    {
        knockBacked = false;
        anim.ResetTrigger("Knock");
    }

    public void BlockStart()
    {
        move.z = zero;
    }

    public void OnHitBlock()
    {
        move = -transform.forward * knockBackForce;
        anim.SetTrigger("HitBlock");
    }

    #endregion

    #region"Hechizos"

    // Reference 
    public IHechizo SpellMethod;

    public void SpellCast()
    {
        SpellMethod.CastSpell();
    }

    #endregion

    #region "Funciones Carecteristicas Player"
    private uint ProbabilidadCritico(Player player)
    {
        uint daño = player.Damage;

        float rnd = Random.Range(zero, hundred);

        if (rnd < player.CritProb || rnd == hundred)
        {
            daño = (uint)(player.Damage * player.CritMult);
            Debug.Log("Fue Critico");
        }

        return daño;
    }

    void RoboDeVida(uint dañoAplicar)
    {
        gm.Player.Life += (uint)((gm.Player.RoboVida / (float)hundred) * dañoAplicar);
    }
    #endregion
}