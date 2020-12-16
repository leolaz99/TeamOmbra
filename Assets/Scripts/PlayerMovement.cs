using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [Tooltip("Velocità del personaggio")]
    [SerializeField] float speed;

    [Tooltip("Velocità della schivata")]
    [SerializeField] float dashSpeed;

    [Tooltip("Tempo di recupero della schivata")]
    [SerializeField] float dashCD;
    
    Rigidbody rb;

    bool candash = true;

    float speedRotation=1000f;  

    Vector3 direction;

    void Move()
    {
        direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
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
        if (Input.GetKeyDown(KeyCode.Space) && candash == true)
        {   
            Dash();  
            StartCoroutine(DashCD());
        }              
    }

    void FixedUpdate()
    {
        Rotation();
        Move();     
    }
}
