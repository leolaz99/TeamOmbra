using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BRAreaInternal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GetComponentInParent<Animator>().SetBool("BR-CanDistance",true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            GetComponentInParent<Animator>().SetBool("BR-CanDistance", false);
        }
    }
}
