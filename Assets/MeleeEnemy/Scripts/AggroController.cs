using UnityEngine;

public class AggroController : MonoBehaviour
{
    public bool isAggro = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isAggro = true;
        }
    }
}
