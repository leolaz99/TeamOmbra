using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BRAggro : StateMachineBehaviour
{
    public float TimerAttack;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        TimerAttack = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(animator.GetComponent<BRControllerIA>().AlertDistance == false)
        {
            animator.GetComponent<BRControllerIA>().agent.isStopped = false;
            animator.GetComponent<BRControllerIA>().agent.SetDestination(animator.GetComponent<BRControllerIA>().Player.transform.position);
            TimerAttack = 0;
        }
        else
        {
            animator.GetComponent<BRControllerIA>().agent.isStopped = true;                 //Ferma il movimento
            TimerAttack += Time.deltaTime;                                                  //Aumenta il timer
            if(TimerAttack >= 5)                                                            //Se il timer è maggiore di un valore TODO: Mettere 5 da inspector
                animator.SetBool("BR-CanAttack", true);                                     //Passa di stato
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
