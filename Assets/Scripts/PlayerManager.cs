using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int life = 100;
    public static PlayerManager instance;

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(collision.gameObject);
        life -= 20;
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
