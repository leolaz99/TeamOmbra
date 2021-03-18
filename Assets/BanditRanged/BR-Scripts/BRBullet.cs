using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BRBullet : MonoBehaviour
{
    public float speed;

    void Start()
    {
        Destroy(gameObject, 5);
    }

    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * speed;
    }
}
