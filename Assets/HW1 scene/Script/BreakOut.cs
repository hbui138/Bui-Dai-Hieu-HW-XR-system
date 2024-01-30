using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class BreakOut : MonoBehaviour
{
    // Start is called before the first frame update
    public InputActionReference action;
    public bool a = true;
    void Start()
    {
        action.action.Enable();
        action.action.performed += (ctx) =>
        {
            if (a == true)
            {
                transform.position = transform.position + new Vector3(0, 20, 0);
                a = false;
            }
            else
            {
                transform.position = transform.position + new Vector3(0, -20, 0);
                a = true;
            }
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
