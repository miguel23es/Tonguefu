using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
 
public class TreeTurn : MonoBehaviour
{
    private float movementX;
    public float speed = 1;

    // This function is called when a move input is detected.
    void OnMove(InputValue movementValue)
    {
        // Convert the input value into a Vector2 for movement.
        Vector2 movementVector = movementValue.Get<Vector2>();

        // Store the X and Y components of the movement.
        movementX = movementVector.x; 
    }
 
    void FixedUpdate()
    {
        if (movementX > 0.4f) transform.Rotate(0.0f, speed, 0.0f);
        else if (movementX < -0.4f) transform.Rotate(0.0f, -speed, 0.0f);


        

    }
}

