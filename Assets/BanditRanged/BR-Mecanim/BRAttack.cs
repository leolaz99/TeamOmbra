﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BRAttack : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        /*Da sostituire con l'evento*/ animator.SetBool("BR-CanAttack", false);
        /*Da sostituire con l'evento*/ Instantiate(animator.GetComponent<BRControllerIA>().Bullet, animator.GetComponent<BRControllerIA>().agent.transform.position, animator.GetComponent<BRControllerIA>().agent.transform.rotation);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //if(animator.GetComponent<BRControllerIA>().AttackDrawLine == true)        //Disegna la linea se è vero
        //{ 
        animator.GetComponent<BRControllerIA>().transform.LookAt(new Vector3(animator.GetComponent<BRControllerIA>().Player.transform.position.x, animator.GetComponent<BRControllerIA>().agent.transform.position.y, animator.GetComponent<BRControllerIA>().Player.transform.position.z));
        Debug.DrawLine(animator.GetComponent<BRControllerIA>().agent.transform.position, animator.GetComponent<BRControllerIA>().Player.transform.position);        //Questo andrebbe in puntamento
        //}
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
