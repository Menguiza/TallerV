using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 3;
    [SerializeField]
    private float jumpFoce = 10f;

    CharacterController characterContrl;
    Animator anim;

    public GameMaster gm;

    BoxCollider weapon;

    bool attack = false;

    //Variables Utilidad
    byte hundred = 100, zero = 0;
    [SerializeField]
    float gravity = 9.8f, vertical, horizontal, verticalVelocity;
    Vector3 move = Vector3.zero;

    void Awake()
    {
        characterContrl = gameObject.GetComponent<CharacterController>();
        anim = gameObject.GetComponent<Animator>();
        weapon = gameObject.GetComponentInChildren<BoxCollider>();
    }

    void Update()
    {
        Move();
        Attack();

        anim.SetFloat("VelocidadAtaque", gm.Player.MultVelAtaque);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            uint dañoAplicar = ProbabilidadCritico(gm.Player);
            uint result = (uint)Mathf.Max(0, other.gameObject.GetComponent<EnemyController>().Life - dañoAplicar);
            other.gameObject.GetComponent<EnemyController>().Life = result;
            RoboDeVida(dañoAplicar);
            gm.enableTGPC = false;
        }
        weapon.enabled = false;
    }

    private void Attack()
    {
        if (Input.GetButton("Punch") && characterContrl.isGrounded)
        {
            anim.SetTrigger("Punch");
            attack = true;
            anim.SetFloat("Horizontal", zero);
            Invoke("ResetTGPC", gm.timeToReset);
        }
    }

    private void Move()
    {
        if (!attack)
        {
            horizontal = Input.GetAxis("Horizontal");

            TurnChar();
        }
        else
        {
            horizontal = zero;

            move = transform.forward * horizontal * (speed * gm.Player.SpeedMult);
        }

        vertical = Input.GetAxisRaw("Vertical");

        anim.SetFloat("Vertical", vertical);

        anim.SetFloat("Horizontal", horizontal);

        SetGravity();

        if (vertical > zero && characterContrl.isGrounded)
        {
            verticalVelocity = jumpFoce;
            anim.SetTrigger("Jump");
            move.y = verticalVelocity;
        }

        if (verticalVelocity <= zero)
        {
            anim.ResetTrigger("Jump");
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

    #region"Metodos de Utilidad"
    public void ActiveCollider()
    {
        weapon.enabled = true;
    }

    public void InactiveCollider()
    {
        weapon.enabled = false;
        attack = false;
        anim.ResetTrigger("Punch");
    }

    private void ResetTGPC()
    {
        gm.enableTGPC = true;
    }
    #endregion

    private uint ProbabilidadCritico(Player player)
    {
        uint daño = player.Damage;

        float rnd = Random.Range(0, 100);

        if (rnd < player.CritProb || rnd == 100)
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
