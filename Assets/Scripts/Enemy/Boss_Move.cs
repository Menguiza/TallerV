using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Move : StateMachineBehaviour
{
    public float speed = 2.5f;
    Transform player;
    Rigidbody rb;
    Boss boss;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameMaster.instance.playerObject.transform;
        rb = animator.transform.parent.GetComponent<Rigidbody>();
        boss = animator.transform.parent.GetComponent<Boss>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (player == null)
        {
            return;
        }
        boss.LookAtPlayer(player.position);
        Vector3 target = new Vector3(rb.position.x, rb.position.y, player.position.z + 3);
        Vector3 newPos = Vector3.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);

        float distanceToWalkPoint = rb.position.z - player.position.z + 3;

        if (distanceToWalkPoint <= 7f)
        {
            animator.SetTrigger("Spawn");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Move");
    }
}
