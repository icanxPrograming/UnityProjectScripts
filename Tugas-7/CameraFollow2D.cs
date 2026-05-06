using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 5f;
    public Vector3 offset;

    Camera cam;
    BoxCollider2D mapBounds;

    float minX, maxX, minY, maxY;

    void Start()
    {
        cam = GetComponent<Camera>();

        GameObject boundsObj = GameObject.Find("MapBounds");
        if (boundsObj != null)
        {
            mapBounds = boundsObj.GetComponent<BoxCollider2D>();
            CalculateBounds();
        }
        else
        {
            Debug.LogError("GameObject 'MapBounds' tidak ditemukan!");
        }
    }

    void CalculateBounds()
    {
        Bounds b = mapBounds.bounds;

        float camHeight = cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;

        minX = b.min.x + camWidth;
        maxX = b.max.x - camWidth;
        minY = b.min.y + camHeight;
        maxY = b.max.y - camHeight;
    }

    void LateUpdate()
    {
        if (target == null || mapBounds == null) return;

        Vector3 desiredPosition = target.position + offset;
        desiredPosition.z = transform.position.z;

        Vector3 smoothPos = Vector3.Lerp(
            transform.position,
            desiredPosition,
            smoothSpeed * Time.deltaTime
        );

        smoothPos.x = Mathf.Clamp(smoothPos.x, minX, maxX);
        smoothPos.y = Mathf.Clamp(smoothPos.y, minY, maxY);

        transform.position = smoothPos;
    }
}