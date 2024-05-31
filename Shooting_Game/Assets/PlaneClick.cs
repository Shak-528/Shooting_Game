using UnityEngine;

public class PlaneClick : MonoBehaviour
{
    public LayerMask planeLayer; // Set this to the layer of your planes in the inspector

    // Update is called once per frame
    void Update()
    {
        // Check if the left mouse button was clicked
        if (Input.GetMouseButtonDown(0))
        {
            // Create a ray from the mouse cursor on screen in the direction of the camera
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, planeLayer);

            // Check if the ray hit this plane
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                // Return the plane to the pool
                PlanePool.Instance.ReturnPlane(gameObject);
            }
        }
    }
}
