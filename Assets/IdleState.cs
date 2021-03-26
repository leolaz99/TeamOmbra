using UnityEngine;

public class IdleState : StateMachineBehaviour
{
    AggroController aggroController;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        aggroController = animator.GetComponentInChildren<AggroController>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (aggroController.isAggro == true)
            animator.SetTrigger("GoToAggro");
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {      
    }
}
