using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int life = 100;
    public int energy = 0;
    public static PlayerManager instance;

    public Collider currentCollider;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "NormalAttack" && collision.gameObject.tag != "ChargeAttack")
        {
            //Destroy(collision.gameObject);
            life -= 20;
            energy = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("AggroArea"))
            currentCollider = other;
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
