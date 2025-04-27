using UnityEngine;

public class RaycastDebugger : MonoBehaviour
{
    void Update()
    {
        // Check if the user clicked the mouse button
        if (Input.GetMouseButtonDown(0))
        {
            // Create a ray from the camera to the point the mouse is pointing at
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            // Check if the ray hit something
            if (hit.collider != null)
            {
                Debug.Log("Ray hit: " + hit.collider.gameObject.name); // Log the hit object
            }
            else
            {
                Debug.Log("Ray did not hit anything.");
            }
        }
    }
}
