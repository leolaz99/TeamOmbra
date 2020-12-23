using UnityEngine;

public class Bullet : MonoBehaviour
{
    GameObject target;

    void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }
    
    void Update()
    {
        transform.Translate(Vector3.forward * 10 * Time.deltaTime);
        transform.LookAt(target.transform);
    }
}
