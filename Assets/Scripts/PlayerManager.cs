using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float life = 1;
    public static PlayerManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Update()
    {
        if (life < 0.2f)
        {
            Destroy(gameObject);
        }
    }
}
