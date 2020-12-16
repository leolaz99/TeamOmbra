using UnityEngine;
using System.Collections;

public class ParrySystem : MonoBehaviour
{

    [Tooltip("Tempo nel quale il parry è attivo")]
    [SerializeField] float parryTimer;

    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
        gameObject.SetActive(false);
    }

    IEnumerator Parry()
    {
        yield return new WaitForSeconds(parryTimer);
        gameObject.SetActive(false);
    }

    void OnEnable()
    {
        StartCoroutine(Parry());
    }
}
