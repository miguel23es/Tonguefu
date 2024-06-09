using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraTurn : MonoBehaviour
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
        //Debug.Log(transform.rotation.eulerAngles.y);
        float yAngle = transform.rotation.eulerAngles.y;

        if (yAngle >= 175f && movementX < 0) {
            transform.RotateAround(new Vector3(0f, 0f, 0f), Vector3.up, speed * movementX);
        }
        else if (yAngle <= 185f && movementX > 0) {
            transform.RotateAround(new Vector3(0f, 0f, 0f), Vector3.up, speed * movementX);
        }

        if (yAngle >= 182f && movementX <= 0) transform.RotateAround(new Vector3(0f, 0f, 0f), Vector3.up, 4 * -speed);
        if (yAngle <= 178f && movementX >= 0) transform.RotateAround(new Vector3(0f, 0f, 0f), Vector3.up, 4 *speed);
    }
}
