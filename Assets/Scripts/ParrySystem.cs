using UnityEngine;
using System.Collections;

public class ParrySystem : MonoBehaviour
{

    [Tooltip("Tempo nel quale il parry è attivo")]
    [SerializeField] float parryTimer;

    PlayerInput playerInput;
    float originalSpeed;

    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
        playerInput.speed = originalSpeed;
        gameObject.SetActive(false);
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
