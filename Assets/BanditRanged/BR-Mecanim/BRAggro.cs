using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BRAggro : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        BRControllerIA.BRController.TimerAttack = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(animator.GetComponent<BRControllerIA>().AlertDistance == false)                                              //Se non è abbastanza vicino
        {
            BRControllerIA.BRController.agent.isStopped = false;                                                        //Rimetto in funzione il movimento
            BRControllerIA.BRController.agent.SetDestination(BRControllerIA.BRController.Player.transform.position);    //Setto la destinazione, cioè il player
            BRControllerIA.BRController.TimerAttack = 0;                                                                //Resetto il timer dell'attacco - Forse non serve
        }
        else
        {
            BRControllerIA.BRController.agent.isStopped = true;                                                         //Ferma il movimento
            BRControllerIA.BRController.TimerAttack += Time.deltaTime;                                                  //Incremento il timer
            if(BRControllerIA.BRController.TimerAttack >= BRControllerIA.BRController.MaxTimerAttack)                   //Se il timer è maggiore di un valore predefinito da inspector
            {
                animator.SetBool("BR-CanAttack", true);                                                                 //Passa dallo stato di aggro allo stato di attacco
            }
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
