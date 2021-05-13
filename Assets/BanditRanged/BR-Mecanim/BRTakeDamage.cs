using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BRTakeDamage : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        BRControllerIA.BRController.ActionRemoveLife();                             //Richiama l'azione per la rimozione della sua vita 
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (BRControllerIA.BRController.AlertDistance == true)                                                                                                      //Se la variabile è vera, quindi il nemico è abbastanza vicino
        {
            animator.Play("BR - Distancing State");                                                                                                                 //Vado nello stato di distanziamento - Controllare parametri essendo questo un cambiamento istantaneo
        }
        else if (BRControllerIA.BRController.EnemyRenderer.isVisible && BRControllerIA.BRController.PlayerInRoom == BRControllerIA.BRController.RoomNumber)         //Se le variabili di controllo della stanza sono vere
        {
            animator.Play("BR - Aggro State");                                                                                                                      //Vado nello stato di aggro - Controllare parametri essendo questo un cambiamento istantaneo
        }
        else if (BRControllerIA.BRController.AlertDistance == false)                                                                                                //Se la variabile è falsa, quindi il nemico è abbastanza lontano
        {
            animator.Play("BR - Attack State");                                                                                                                     //Vado nello stato di attacco - Controllare parametri essendo questo un cambiamento istantaneo
        }
    }

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
