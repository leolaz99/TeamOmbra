using UnityEngine;

public class AggroController : MonoBehaviour
{
    public bool isAggro = false;
    bool isVisible = false;

    Camera cam;

    void CameraVisible()
    {
        Vector3 screenPoint = cam.WorldToViewportPoint(transform.position);

        if (screenPoint.y < 0 || screenPoint.y > 1 || screenPoint.x < 0 || screenPoint.x > 1)
        {
            isVisible = false;
        }

        else
            isVisible = true;
    }


    void Awake()
    {
        cam = FindObjectOfType<Camera>();
    }

    void Update()
    {
        if (PlayerManager.instance.currentCollider == GetComponent<EnemyManager>().aggroArea && isVisible == true)
        {
            isAggro = true;
        }

        CameraVisible();
    }
}
