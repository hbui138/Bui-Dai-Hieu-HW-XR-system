using UnityEngine;

public class WeaponCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collided object is an enemy
        if (other.CompareTag("Enemy"))
        {
            // Get the SkinnedMeshRenderer component of the enemy
            SkinnedMeshRenderer skinnedMeshRenderer = other.GetComponent<SkinnedMeshRenderer>();

            // If the SkinnedMeshRenderer exists, disable it and enable the MeshRenderer
            if (skinnedMeshRenderer != null)
            {
                skinnedMeshRenderer.enabled = false;
                MeshRenderer meshRenderer = other.GetComponent<MeshRenderer>();
                if (meshRenderer != null)
                {
                    meshRenderer.enabled = true;
                }
                else
                {
                    Debug.LogError("MeshRenderer component not found on the enemy GameObject.");
                }
            }
            else
            {
                Debug.LogError("SkinnedMeshRenderer component not found on the enemy GameObject.");
            }
        }
    }
}

