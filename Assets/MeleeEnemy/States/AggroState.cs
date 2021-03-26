using UnityEngine;

public class AggroState : StateMachineBehaviour
{
    EnemyMove enemyMove;
    AttackController attackController;
    [SerializeField] float aggroSpeed;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyMove = animator.GetComponent<EnemyMove>();
        attackController = animator.GetComponentInChildren<AttackController>();
        enemyMove.navMeshAgent.isStopped = false;
        attackController.isAttack = false;
        enemyMove.navMeshAgent.speed = aggroSpeed;      
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyMove.SetDestination();
        
        if (attackController.isAttack == true)
        {
            enemyMove.navMeshAgent.isStopped = true;
            attackController.isAttack = false;
            animator.SetTrigger("GoToAttack");
        }               
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {       
    }
}
