using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnifyingGlassCameraPosition : MonoBehaviour
{
    public Transform mainCameraTransform;

    void Update()
    {
        Vector3 directionToMainCamera = mainCameraTransform.position - transform.position;

        Vector3 oppositeDirection = -directionToMainCamera;

        transform.rotation = Quaternion.LookRotation(oppositeDirection.normalized);
    }
}
