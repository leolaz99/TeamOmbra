using UnityEngine;
using System.Collections;

public class ParrySystem : MonoBehaviour
{
    [SerializeField] GameObject parryCollider;
    
    [Tooltip("Tempo nel quale il parry è attivo")]
    [SerializeField] float parryTimer;

    [Tooltip("Tempo nel quale sei scoperto dopo un parry a vuoto")]
    [SerializeField] float parryCD;
    bool cd = false;
    
    bool isParry = false;

    void OnCollisionEnter(Collision collision)
    {
        Destroy(collision.gameObject);
        
        if (parryCollider.activeSelf == true)
        {          
            parryCollider.SetActive(false);
            isParry = false;
            StopCoroutine(Parry());
        }

        else
        {
            PlayerManager.instance.life = PlayerManager.instance.life - 0.2f;
        }         
    }

    IEnumerator Parry()
    {
        isParry = true;
        parryCollider.SetActive(true);
        yield return new WaitForSeconds(parryTimer);
        parryCollider.SetActive(false);
        isParry = false;
        StartCoroutine(ParryCD());
    }

    IEnumerator ParryCD()
    {
        cd = true;
        yield return new WaitForSeconds(parryCD);
        cd = false;
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(1) && isParry == false && cd == false)
        {
            StartCoroutine(Parry());        
        }
    }
}
