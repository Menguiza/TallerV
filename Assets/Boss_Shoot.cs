using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Shoot : StateMachineBehaviour
{
    Transform player;
    Rigidbody rb;
    Boss boss;

    public float timeToSpawn, speed = 4f, timer;
    int count = 0;

    bool change = false;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameMaster.instance.playerObject.transform;
        rb = animator.transform.parent.GetComponent<Rigidbody>();
        boss = animator.transform.parent.GetComponent<Boss>();
        timer = 0;
        change = false;
        count = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (player == null)
        {
            return;
        }
        Vector3 target = new Vector3(rb.position.x, boss.altitude, player.position.z + 3);
        Vector3 newPos = Vector3.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);

        if (count < 5)
        {
            timer += Time.deltaTime;

            if (timer >= timeToSpawn)
            {
                boss.Attack();
                count++;
                timer = 0;
            }
        }

        if (count == 5 && !change)
        {
            change = true;
            animator.SetTrigger("Move");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Shoot");
    }
}
