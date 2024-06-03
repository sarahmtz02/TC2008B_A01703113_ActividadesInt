using UnityEngine;
using System.Collections;

public class BulletDestroyer : MonoBehaviour
{
    private Camera mainCamera;
    private Renderer bulletRenderer;

    void Start()
    {
        mainCamera = Camera.main;
        bulletRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        if (!IsObjectVisible())
        {
            Destroy(gameObject);
        }
    }

    bool IsObjectVisible()
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(mainCamera);
        return GeometryUtility.TestPlanesAABB(planes, bulletRenderer.bounds);
    }
}