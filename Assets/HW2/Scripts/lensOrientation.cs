using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lensOrientation : MonoBehaviour
{

    void Update()
    {
        Quaternion currentRotation = transform.rotation;
        transform.rotation = Quaternion.Euler(currentRotation.eulerAngles.x, currentRotation.eulerAngles.y, 0f);
    }
}
