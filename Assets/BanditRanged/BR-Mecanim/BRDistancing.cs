using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BRDistancing : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        BRControllerIA.BRController.transform.LookAt(new Vector3(BRControllerIA.BRController.Player.transform.position.x, BRControllerIA.BRController.agent.transform.position.y, BRControllerIA.BRController.Player.transform.position.z));    //Per tutta la durata dello stato guarderà il player
        BRControllerIA.BRController.agent.Move(-BRControllerIA.BRController.agent.transform.forward * Time.deltaTime * BRControllerIA.BRController.SpeedDistancing);                                                                            //Si muoverà nella distanza opposta rispetto al player
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
