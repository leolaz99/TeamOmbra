using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float life = 1;
    public static PlayerManager instance;

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(collision.gameObject);
        life = life - 0.2f;
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
