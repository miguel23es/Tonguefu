using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;


public class FrogMovementBoss : MonoBehaviour
{
    // Rigidbody of the player.
    public Rigidbody rb; 

    public PlayerHealth playerHealth;

    // Player model
    public Transform playerModel;
    float lastMovement;

    // Movement along X and Y axes.
    private float movementX;
    private float movementY;

    // Jump
    public bool isGrounded;
    public float jumpForce = 1f;
    public LayerMask floorLayer; 
    private float mayJump;
    
    // Tree
    public Transform target;

    Vector3 Vec;

    // Enemy
    public float knockback = 20f;
    private bool canBeHit = true;
    private float hitCooldownTime = 0.75f;


    // Start is called before the first frame update.
    void Start()
    {
        
        // Get and store the Rigidbody component attached to the player.

    }
 
    // This function is called when a move input is detected.
    void OnMove(InputValue movementValue)
    {
        // Convert the input value into a Vector2 for movement.
        Vector2 movementVector = movementValue.Get<Vector2>();

        // Store the X and Y components of the movement.
        movementX = movementVector.x; 
        movementY = movementVector.y; 

    }


    // FixedUpdate is called once per fixed frame-rate frame.
    private void FixedUpdate() 
    {
        // JUMP

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.1f, floorLayer)) isGrounded = true;
        else isGrounded = false;

        if (isGrounded) mayJump = 0.15f;
        else mayJump -= Time.deltaTime;

        
        if (rb.velocity.y < 0f && rb.velocity.y > -10f) rb.velocity += new Vector3(0f, -Physics.gravity.y * (-1.7f) * Time.deltaTime, 0f);


        
        if (mayJump > 0f && movementY > 0f)
            //rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            rb.velocity = new Vector3(0f, jumpForce, 0f);
            //rb.AddForce(Vector3.up * jumpForce);

        // move left and right
        Vec = transform.localPosition;
        Vec.x += Input.GetAxis("Horizontal") * Time.deltaTime * 5;
        transform.localPosition = Vec;
    


        // PLAYER MODEL

        // Player model rotate
        
         //Flip if moving right
        if (lastMovement > 0) {

            // Apply the rotation to the player model's transform instantly
            playerModel.rotation = Quaternion.Euler(-90f, 0f, -90f);
        }
        else {
            playerModel.rotation = Quaternion.Euler(-90f, 0f, 90f);
        }

        // Record last Movement
        if (movementX != 0)
            lastMovement = movementX;

    }

    // Hit cooldown
     IEnumerator HitCooldown()
    {
        // Disable knockback for the specified duration
        canBeHit = false;

        // Wait for the cooldown duration
        yield return new WaitForSeconds(hitCooldownTime);

        // Enable knockback again
        canBeHit = true;
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Enemy") && canBeHit) {
            StartCoroutine(HitCooldown());
            playerHealth.TakeDamage(1);
            Debug.Log("hit");

            Vector3 direction = (transform.position - other.transform.position).normalized;

            Debug.Log(direction);

            // Handle x knockback
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector3.right * knockback * direction.z * 0.35f);

            // Handle y knockback
            rb.AddForce(Vector3.up * knockback * direction.y * 0.35f);     
        }

        /*
        if (other.gameObject.CompareTag("Wall")) {
            Debug.Log("wall");
            Vector3 direction = (transform.position - other.transform.position).normalized;

            if (direction.x < 0) 
                target.transform.Rotate(0.0f, 1f, 0.0f);
            else
                target.transform.Rotate(0.0f, -1f, 0.0f);
        }*/
    }


}