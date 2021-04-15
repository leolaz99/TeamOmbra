using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BRPC : MonoBehaviour
{
    public float SpeedBRPC = 160f;

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * SpeedBRPC * Time.deltaTime * v);
        transform.Translate(Vector3.right * SpeedBRPC * Time.deltaTime * h);
    }
}
