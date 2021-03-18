using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BRAreaSecurityDistance : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GetComponentInParent<BRControllerIA>().AlertDistance = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            GetComponentInParent<BRControllerIA>().AlertDistance = false;
        }
    }
}
