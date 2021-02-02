using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour
{
    [Tooltip("Velocità del personaggio")]
    public float speed;

    [Tooltip("Velocità della schivata")]
    [SerializeField] float dashSpeed;

    [Tooltip("Tempo di recupero della schivata")]
    [SerializeField] float dashCD;

    [SerializeField] GameObject parryCollider;

    Rigidbody rb;
    bool candash = true;
    float speedRotation=1000f;  
    Vector3 direction;

    void Move()
    {
        if(GetComponentInChildren<AttackSystem>().CanOnlyMove == true)
        {
            direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        }
        else
        {
            direction = new Vector3(0, 0, 0);
        }

        rb.velocity = direction.normalized * speed * Time.deltaTime;
    }

    void Dash()
    {
        rb.AddForce(direction * dashSpeed, ForceMode.VelocityChange);
        candash = false;
    }

    void Rotation()
    {
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


        float hitdist = 0.0f;
        if (playerPlane.Raycast(ray, out hitdist))
        {
            Vector3 targetPoint = ray.GetPoint(hitdist);
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speedRotation * Time.deltaTime);
        }
    }

    void Parry()
    {
        parryCollider.SetActive(true);
    }

    IEnumerator DashCD()
    {            
        yield return new WaitForSeconds(dashCD);
        candash = true;
    }


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Parry();
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && candash == true)
        {
            Dash();
            StartCoroutine(DashCD());
        }      
        
        Rotation();
    }

    void FixedUpdate()
    {  
        Move();            
    }
}
