using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Dash : StateMachineBehaviour
{
    Transform player;
    Rigidbody rb;
    Boss boss;

    public float timeToDash, speed = 4f, timer;
    bool dashing, goForIt, ohNo;

    Vector3 objective = Vector3.zero;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameMaster.instance.playerObject.transform;
        rb = animator.transform.parent.GetComponent<Rigidbody>();
        boss = animator.transform.parent.GetComponent<Boss>();
        timer = 0;
        dashing = false;
        goForIt = false;
        ohNo = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (player == null)
        {
            return;
        }

        if (!dashing)
        {
            Vector3 target = new Vector3(rb.position.x, rb.position.y, player.position.z + 3);
            Vector3 newPos = Vector3.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);

            boss.LookAtPlayer(player.position);
        }

        if(dashing)
        {
            if(!goForIt)
            {
                Vector3 target = new Vector3(rb.position.x, player.position.y - 1.4f, rb.position.z);
                Vector3 newPos = Vector3.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
                rb.MovePosition(newPos);

                boss.LookAtPlayer(target);
            }

            timer += Time.deltaTime;

            if (timer >= timeToDash)
            {
                goForIt = true;
            }
        }

        Dash();

        if (rb.position == objective && goForIt)
        {
            goForIt = false;
            animator.SetTrigger("Shoot");
        }

        if (rb.position.z == player.position.z + 3 && !dashing)
        {
            dashing = true;
            objective = player.position;
            objective.y = objective.y - 1.4f;
        }
    }

    void Dash()
    {
        if(goForIt)
        { 
            if(!ohNo)
            {
                ohNo = boss.CollisionDetection();
            }

            Vector3 newPos = Vector3.MoveTowards(rb.position, objective, (speed * 2) * Time.fixedDeltaTime);
            rb.MovePosition(newPos);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Dash");
    }
}
