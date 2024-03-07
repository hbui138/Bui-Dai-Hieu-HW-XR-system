using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;

public class SliceObject : MonoBehaviour
{
    public Transform startSlicePoint;
    public Transform endSlicePoint;
    public VelocityEstimator velocityEstimator;
    public LayerMask sliceable;
    public float cutForce = 10;
    public Material crossSection;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bool hasHit = Physics.Linecast(startSlicePoint.position, endSlicePoint.position, out RaycastHit hit, sliceable);
        if (hasHit)
        {
            GameObject target = hit.transform.gameObject;
            Slice(target);
        }
    }

    public void Slice(GameObject target)
    {
        Vector3 velocity = velocityEstimator.GetVelocityEstimate();
        Vector3 plane = Vector3.Cross(endSlicePoint.position - startSlicePoint.position, velocity);
        plane.Normalize();
        SlicedHull hull = target.Slice(endSlicePoint.position, plane);

        if(hull != null)
        {
            GameObject upperHull = hull.CreateUpperHull(target, crossSection);
            SetUpSlicedComponent(upperHull);
            upperHull.transform.position = target.transform.position; // Set position of upper hull
            CopyMaterial(target, upperHull); // Copy material from original object to upper hull

            GameObject lowerHull = hull.CreateLowerHull(target, crossSection);
            SetUpSlicedComponent(lowerHull);
            lowerHull.transform.position = target.transform.position; // Set position of lower hull
            CopyMaterial(target, lowerHull); // Copy material from original object to lower hull

            Destroy(target);
        }
    }
    public void CopyMaterial(GameObject original, GameObject target)
    {
        Renderer originalRenderer = original.GetComponent<Renderer>();
        if (originalRenderer != null)
        {
            Renderer targetRenderer = target.GetComponent<Renderer>();
            if (targetRenderer != null && originalRenderer.material != null)
            {
                targetRenderer.material = originalRenderer.material;
            }
        }
    }

    public void SetUpSlicedComponent(GameObject slicedObject)
    {
        //slicedObject.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        Rigidbody rb = slicedObject.AddComponent<Rigidbody>();
        MeshCollider collider = slicedObject.AddComponent<MeshCollider>();
        collider.convex = true;

        // Destroy the sliced object after 3 seconds
        Destroy(slicedObject, 3f);
    }
}
