using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] Transform spawn1;
    [SerializeField] Transform spawn2;

    void Update()
    {
        float rand = Random.Range(spawn1.position.z, spawn2.position.z);
        Instantiate(prefab, new Vector3(transform.position.x, transform.position.y, rand), transform.rotation);
        gameObject.SetActive(false);
    }
}
