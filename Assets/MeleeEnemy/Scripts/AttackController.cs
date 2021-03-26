using UnityEngine;

public class AttackController : MonoBehaviour
{
    public bool isAttack = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isAttack = true;
        }
    }
}
