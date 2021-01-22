using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int life = 100;
    public int energy = 0;
    public static PlayerManager instance;

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(collision.gameObject);
        life -= 20;
        energy = 0;
    }
    
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Update()
    {
        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }
}
