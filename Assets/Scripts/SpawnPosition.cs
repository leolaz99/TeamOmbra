using UnityEngine;

public class SpawnPosition : MonoBehaviour
{
    [SerializeField] GameObject spawn1;
    [SerializeField] GameObject spawn2;
    [SerializeField] GameObject spawn3;
    [SerializeField] GameObject spawn4;
    float timer = 0;

    void Update()
    {
        timer = timer + Time.deltaTime;
        if (timer >= 2)
        {
            int rand = Random.Range(1, 5);
            if (rand == 1)
                spawn1.SetActive(true);
            
            if (rand == 2)
                spawn2.SetActive(true);
            
            if (rand == 3)
                spawn3.SetActive(true);
           
            if (rand == 4)
                spawn4.SetActive(true);

            timer = 0;
        }
    }
}
