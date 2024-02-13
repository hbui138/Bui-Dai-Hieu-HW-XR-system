using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Glass : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;
    public Transform lens;
    public Transform camera;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate the camera every frame so it keeps looking at the target
        Vector3 vector1 = player.position;
        Vector3 vector2 = lens.position;
        camera.transform.LookAt(vector1 - vector2);

    }
}
