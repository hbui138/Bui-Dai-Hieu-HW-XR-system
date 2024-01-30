using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ToggleLight : MonoBehaviour
{
    public new Light light;
    public InputActionReference action;
    public bool a = true;
    void Start()
    {
        light.GetComponent<Light>();
        action.action.Enable();
        action.action.performed += (ctx) =>
        {
            if (a == true)
            {
                light.color = Color.green;
                a = false;
            }
            else
            {
                light.color = Color.white;
                a = true;
            }
        };
    }

    void Update()
    {
    
    }
}
