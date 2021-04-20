using UnityEngine;

public class AttackState : StateMachineBehaviour
{
    [SerializeField] float attackSpeed;
    EnemyManager enemyManager;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyManager = animator.GetComponent<EnemyManager>();
        enemyManager.isParryed = false;
        enemyManager.isAttacking = true;
        animator.SetBool("GoToRecovery", true);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(enemyManager.isParryed != true)
            animator.transform.Translate(Vector3.forward * attackSpeed * Time.deltaTime);
        
        if(enemyManager.isParryed == true)
        {
            animator.SetBool("GoToStun", true);
            animator.SetBool("GoToRecovery", false);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyManager.isAttacking = false;
    }
}
