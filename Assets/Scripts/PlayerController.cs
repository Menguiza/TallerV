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

    CharacterController characterContrl;
    Animator anim;

    public GameMaster gm;

    bool attack = false, airAttack = false, dodge = false, dodgeEnable = true, knockBacked = false;
    public bool blocking = false;

    //Variables Utilidad
    byte hundred = 100, zero = 0, fullTurn = 180;

    [SerializeField]
    private float jumpFoce = 10f, resetDodgeTime = 2f, dodgeForce = 100f, knockBackForce = 5f;

    [SerializeField]
    float gravity = 9.8f, attackRange = 1f;
    float horizontal, verticalVelocity;

    Vector3 move = Vector3.zero;

    void Awake()
    {
        characterContrl = gameObject.GetComponent<CharacterController>();
        anim = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        Attack();

        anim.SetFloat("VelocidadAtaque", gm.Player.MultVelAtaque);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.collider.CompareTag("Enemy"))
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
        }
    }

    private void Attack()
    {
        if (Input.GetButtonDown("Punch") && characterContrl.isGrounded && !attack && !dodge && !knockBacked && !blocking && !airAttack)
        {
            anim.SetTrigger("Punch");

            attack = true;

            anim.SetFloat("Horizontal", zero);
        }
        else if(Input.GetButtonDown("Punch") && !characterContrl.isGrounded && !dodge && !knockBacked && !airAttack && !attack)
        {
            airAttack = true;
            anim.SetTrigger("AirAttack");
        }
    }

    private void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
        {
            return;
        }

        Gizmos.DrawSphere(attackPoint.position, attackRange);
    }

    private void Move()
    {
        if (attack && !knockBacked)
        {
            horizontal = zero;

            move.z = horizontal;
        }
        else if (!characterContrl.isGrounded && !knockBacked && !blocking)
        {
            if (Input.GetAxisRaw("Horizontal") != zero)
            {
                horizontal = Input.GetAxisRaw("Horizontal"); 
            }

            if (Input.GetButtonDown("Dodge") && !dodge && horizontal !=zero)
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
            else if(dodge)
            {
                TurnCharDash();
            }
        }
        else if (!knockBacked)
        {
            if (Input.GetButtonDown("Dodge") && dodgeEnable && Input.GetAxisRaw("Horizontal") != zero && !knockBacked && !blocking)
            {
                dodge = true;
                dodgeEnable = false;
                anim.SetTrigger("DodgeGround");
                horizontal = Input.GetAxisRaw("Horizontal");

                if(horizontal<zero)
                {
                    move.z -= dodgeForce * Time.deltaTime;
                }
                else if(horizontal>zero)
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
                else if(!dodge)
                {
                    horizontal = Input.GetAxisRaw("Horizontal");
                    TurnChar();
                    blocking = false;
                    anim.SetBool("Block", false);
                }
                else if(dodge)
                {
                    TurnCharDash();
                }
            }
        }

        SetGravity();

        anim.SetFloat("Horizontal", Input.GetAxis("Horizontal"));

        if (Input.GetButtonDown("Jump") && characterContrl.isGrounded && !attack && !dodge && !knockBacked)
        {
            verticalVelocity = jumpFoce;
            anim.SetBool("Jump", true);
            move.y = verticalVelocity;
        }
        else if(knockBacked && characterContrl.isGrounded)
        {
            verticalVelocity = jumpFoce;
            move.y = verticalVelocity;
        }

        if (characterContrl.isGrounded && verticalVelocity<zero)
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

            if(gm.Player.Status == GameMaster.estado.Dormido && gm.Player.Conciencia<gm.Player.MaxConciencia)
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

    #region"Ataque Saltando"
    /*
    if (!attack)
        {
            horizontal = Input.GetAxis("Horizontal");

            if (horizontal < 0)
            {
                transform.rotation = new Quaternion(0, -180, 0, 0);
                move = -transform.forward * Mathf.Round(horizontal) * (speed * gm.Player.SpeedMult);
            }
            else if (horizontal > 0)
            {
                transform.rotation = new Quaternion(0, 0, 0, 0);
                move = transform.forward * Mathf.Round(horizontal) * (speed * gm.Player.SpeedMult);
            }
        }
        else
        {
            if (horizontal != 0)
            {
                if(!characterContrl.isGrounded)
                {
                    horizontal -= horizontal * Time.deltaTime;
                }
                else
                {
                    horizontal = 0;
                }

                if(transform.rotation.y == 0)
                {
                    move = transform.forward * horizontal * (speed * gm.Player.SpeedMult);
                }
                else
                {
                    move = -transform.forward * horizontal * (speed * gm.Player.SpeedMult);
                }
            }
        }

        float vertical = Input.GetAxisRaw("Vertical");

        anim.SetFloat("Vertical", vertical);

        anim.SetFloat("Horizontal", horizontal);

        if (characterContrl.isGrounded)
        {
            verticalVelocity = -gravity * Time.deltaTime;

            if (vertical > 0)
            {
                verticalVelocity = jumpFoce;
                anim.SetTrigger("Jump");
            }
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        if (verticalVelocity <= 0)
        {
            anim.ResetTrigger("Jump");
        }

        move.y = verticalVelocity;

        characterContrl.Move(move * Time.deltaTime);
     */
    #endregion
}
