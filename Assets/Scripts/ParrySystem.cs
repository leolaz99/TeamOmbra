using UnityEngine;
using System.Collections;

public class ParrySystem : MonoBehaviour
{

    [Tooltip("Tempo nel quale il parry è attivo")]
    [SerializeField] float parryTimer;

    [Tooltip("Energia guardagnata eseguendo il parry correttamente")]
    [SerializeField] int parryGainEnergy = 10;

    PlayerInput playerInput;
    float originalSpeed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            //Destroy(other.gameObject);
            EnemyManager.instance.isParryed = true;
            playerInput.speed = originalSpeed;
            gameObject.SetActive(false);
            PlayerManager.instance.energy += parryGainEnergy;
        }

        //if (other.gameObject.CompareTag("Bullet"))
        //{
        //    Destroy(other.gameObject);
        //    playerInput.speed = originalSpeed;
        //    gameObject.SetActive(false);
        //    PlayerManager.instance.energy += parryGainEnergy;
        //}
    }

    IEnumerator Parry()
    {
        yield return new WaitForSeconds(parryTimer);
        playerInput.speed = originalSpeed;
        gameObject.SetActive(false);
    }


    void Awake()
    {
        playerInput = FindObjectOfType<PlayerInput>();
    }

    void OnEnable()
    {
        originalSpeed = playerInput.speed;
        playerInput.speed = 0;
        StartCoroutine(Parry());
    }
}
