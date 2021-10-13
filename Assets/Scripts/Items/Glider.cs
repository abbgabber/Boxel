using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Glider : MonoBehaviour
{
    public Movement movement;

    void Start()
    {
        print(movement.gravity);
    }
    private void Update()
    {
        if (Input.GetMouseButton(0) && !movement.isGrounded)
        {
            movement.gravity = -2f;
            if (movement.jumpAxis.y > 0.5f)
            {
                movement.jumpAxis.y = 0f;
            }
        }
        else if (movement.gravity != -9.82f)
        {
            movement.gravity = -9.82f;
        }
    }
}
