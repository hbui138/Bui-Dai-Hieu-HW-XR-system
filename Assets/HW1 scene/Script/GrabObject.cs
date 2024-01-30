using UnityEngine;
using UnityEngine.InputSystem;

public class GrabObject : MonoBehaviour
{
    public float grabRadius = 0.5f; // Radius for detecting nearby objects
    public LayerMask grabMask; // Layer mask for detecting nearby objects

    public Transform grabbedObject; // Reference to the grabbed object
    public Vector3 grabbedObjectPos; // Position of the grabbed object relative to the controller
    public Quaternion grabbedObjectRot; // Rotation of the grabbed object relative to the controller
    public InputActionReference grabAction; // Input action for grabbing
    
    private void Awake()
    {
        // Set up input action for grabbing
        grabAction.action.performed += ctx => Grab();
        grabAction.action.canceled += ctx => Release();
    }

    private void OnEnable()
    {
        // Enable input action for grabbing
        grabAction.action.Enable();
    }

    private void OnDisable()
    {
        // Disable input action for grabbing
        grabAction.action.Disable();
    }

    private void Update()
    {
        // Check if an object is nearby
        Collider[] colliders = Physics.OverlapSphere(transform.position, grabRadius, grabMask);
        if (colliders.Length > 0)
        {
            // Grab the nearest object
            Transform nearest = colliders[0].transform;
            float minDistance = Vector3.Distance(transform.position, nearest.position);
            for (int i = 1; i < colliders.Length; i++)
            {
                float distance = Vector3.Distance(transform.position, colliders[i].transform.position);
                if (distance < minDistance)
                {
                    nearest = colliders[i].transform;
                    minDistance = distance;
                }
            }

            // Check if we are grabbing the object
            if (grabAction.action.IsPressed() && grabbedObject == null)
            {
                Grab(nearest);
            }
        }
        else
        {
            // Release the object if there are no nearby objects
            Release();
        }

        // Move and rotate the grabbed object
        if (grabbedObject != null)
        {
            grabbedObject.position = transform.position + grabbedObjectPos;
            grabbedObject.rotation = transform.rotation * grabbedObjectRot;
        }
    }

    private void Grab(Transform objectToGrab = null)
    {
        // Set the grabbed object
        grabbedObject = objectToGrab ?? grabbedObject;
        if (grabbedObject == null) return;

        // Set the position and rotation of the grabbed object relative to the controller
        grabbedObjectPos = grabbedObject.position - transform.position;
        grabbedObjectRot = Quaternion.Inverse(transform.rotation) * grabbedObject.rotation;
    }

    private void Release()
    {
        // Clear the grabbed object
        grabbedObject = null;
    }

    private void OnDrawGizmosSelected()
    {
        // Draw the grab radius in the editor
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, grabRadius);
    }
}
