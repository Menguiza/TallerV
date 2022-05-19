using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Spawn : StateMachineBehaviour
{
    Transform player;
    Rigidbody rb;
    Boss boss;

    public float timer, timeToSpawn;
    int count;
    bool change;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameMaster.instance.playerObject.transform;
        rb = animator.transform.parent.GetComponent<Rigidbody>();
        boss = animator.transform.parent.GetComponent<Boss>();
        timer = 0;
        change = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (count < 3)
        {
            timer += Time.deltaTime;

            if (timer >= timeToSpawn)
            {
                // Spawnear enemigo
                boss.Spawn();
                count++;
                timer = 0;
            }
        }

        if (count == 3 && !change)
        {
            change = true;
            animator.SetTrigger("Dash");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Spawn");
    }
}
