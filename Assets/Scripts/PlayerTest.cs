using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;


public class PlayerTest : MonoBehaviour
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
    public Collider cube;
    
    // Tree
    public Transform target;

    // Enemy
    public float knockback = 20f;
    private bool canBeHit = true;
    private bool beingPushed = false;
    private float hitCooldownTime = 0.75f;
    private Transform enemy;



    // Start is called before the first frame update.
    void Start()
    {
        //winTextObject.SetActive(false);
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
        Vector3 left = new Vector3(cube.bounds.min.x, cube.bounds.center.y, cube.bounds.center.z);
        Vector3 right = new Vector3(cube.bounds.max.x, cube.bounds.center.y, cube.bounds.center.z);
       
        bool leftGrounded = Physics.Raycast(left, Vector3.down, 0.6f, floorLayer);
        bool rightGrounded = Physics.Raycast(right, Vector3.down, 0.6f, floorLayer);
        bool centerGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f, floorLayer);
        
        //RaycastHit hit;
        if (leftGrounded || rightGrounded || centerGrounded) isGrounded = true;
        else isGrounded = false;

        if (isGrounded) mayJump = 0.15f;
        else mayJump -= Time.deltaTime;

        
        if (rb.velocity.y < 0f && rb.velocity.y > -10f) rb.velocity += new Vector3(0f, -Physics.gravity.y * (-1.7f) * Time.deltaTime, 0f);


        
        if (mayJump > 0f && movementY > 0f)
            //rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            rb.velocity = new Vector3(0f, jumpForce, 0f);
            //rb.AddForce(Vector3.up * jumpForce);

        if (beingPushed)
        {
            Vector3 direction = (transform.position - enemy.transform.position).normalized;

            // Handle x knockback
            if (direction.x < 0)
                target.transform.Rotate(0.0f, -direction.x * knockback * Time.deltaTime, 0.0f);
            else
                target.transform.Rotate(0.0f, -direction.x * knockback * Time.deltaTime, 0.0f);

            
        }


        // PLAYER MODEL

        // Player model rotate
        
        // Flip if moving right
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
        // Disable being for the specified duration
        canBeHit = false;

        // Wait for the cooldown duration
        yield return new WaitForSeconds(hitCooldownTime);

        // Enable being hit again
        canBeHit = true;
    }

    // Hit cooldown
    IEnumerator KnockbackTimer()
    {
        beingPushed = true;

        // Wait for the cooldown duration
        yield return new WaitForSeconds(0.25f);

        // Disable knockback
        beingPushed = false;
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Enemy") && canBeHit) {
            StartCoroutine(HitCooldown());
            StartCoroutine(KnockbackTimer());
            playerHealth.TakeDamage(1);
            Debug.Log("hit");

            
            enemy = other.transform;


            // Handle y knockback
            Vector3 direction = (transform.position - enemy.transform.position).normalized;
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector3.up * knockback * direction.y * 0.35f);


        }

        if (other.gameObject.CompareTag("Wall")) {
            Debug.Log("wall");
            Vector3 direction = (transform.position - other.transform.position).normalized;

            if (direction.x < 0) 
                target.transform.Rotate(0.0f, 1f, 0.0f);
            else
                target.transform.Rotate(0.0f, -1f, 0.0f);
        }
    }
    
    private void OnCollisionStay(Collision other) {
        if (other.gameObject.CompareTag("Wall")) {
            Debug.Log("wall");
            Vector3 direction = (transform.position - other.transform.position).normalized;

            if (direction.x < 0) 
                target.transform.Rotate(0.0f, 1f, 0.0f);
            else
                target.transform.Rotate(0.0f, -1f, 0.0f);
        }
    }

    
 
}