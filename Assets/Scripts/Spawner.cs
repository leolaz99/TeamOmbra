using UnityEngine;

public class Spawner : MonoBehaviour
{
    float timer = 0;
    [SerializeField] GameObject prefab;

    void Update()
    {
        timer = timer + Time.deltaTime;
        if (timer >= 1)
        {
            Instantiate(prefab, transform.position, transform.rotation);
            timer = 0;
        }
    }
}
