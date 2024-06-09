using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using UnityEngine.Assertions.Must;

public class Grapple : MonoBehaviour
{
    public GameObject hookPrefab; 
    public Transform shootPoint; 
    public Transform Tree;
    public PlayerHealth playerHealth;
    public AudioClip slurp;

    private GameObject currentHook;
    private Transform targetObject;
    private LineRenderer lr;
    private Rigidbody rb;
    private float movementX;
    private GameObject ragdollModel;
    private bool canHook = true;
    private bool bee = true;

    void OnMove(InputValue movementValue)
    {
        // Convert the input value into a Vector2 for movement.
        Vector2 movementVector = movementValue.Get<Vector2>();

        // Store the X and Y components of the movement.
        movementX = movementVector.x;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        
        if (Input.GetMouseButtonDown(1) && canHook) 
        {
            if (currentHook != null) Destroy(currentHook);
            ShootHook();
        }
        // Pull dead enemy
        if (ragdollModel != null)
        {
            ragdollModel.transform.position = Vector3.MoveTowards(ragdollModel.transform.position, transform.position - new Vector3(0f, 0.5f, 0f), 0.1f);
            ragdollModel.transform.Rotate(5f, 5f, 0f);
            lr.SetPosition(0, shootPoint.position);
            lr.SetPosition(1, ragdollModel.transform.position);
            if (Vector3.Distance(shootPoint.position, ragdollModel.transform.position) < 1.5f)
            {
                
                

                if (bee == true) playerHealth.TakeDamage(1);
                else playerHealth.Heal(1);
                

                Destroy(ragdollModel);
                Destroy(currentHook);
            }
            
        }
        else if (currentHook != null)
        {
            
            float distanceX = Vector3.Distance(new Vector3(transform.position.x, 0f, transform.position.z), new Vector3(targetObject.position.x, 0f, targetObject.position.z));
            float distanceY = Vector3.Distance(new Vector3(0f, transform.position.y, 0f), new Vector3(0f, targetObject.position.y, 0f));
            Vector3 direction = (targetObject.position - transform.position);
            if (distanceX > 9f || distanceX < -9f) Destroy(currentHook);

            lr.SetPosition(0, shootPoint.position);
            lr.SetPosition(1, targetObject.position);

            // Bring player towards branch
            
            if (direction.y > 0 && rb.velocity.y < 7f)
            {
                rb.velocity += new Vector3(0f, 8f * distanceY * Time.deltaTime, 0f);
            }
            if (direction.x < 0f) Tree.Rotate(0.0f, 1.25f * (distanceX * distanceX) * Time.deltaTime, 0.0f);
            if (direction.x > 0f) Tree.Rotate(0.0f, -1.25f * (distanceX * distanceX) * Time.deltaTime, 0.0f);

            
        }
    }

    IEnumerator swallow()
    {
        canHook = false;
        yield return new WaitForSeconds(0.4f);
        canHook = true;

    }

    void ShootHook()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 12f, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Ignore))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                if (currentHook != null)
                {
                    return;
                }
                // bee or fly
                FlyEnemy script = hit.collider.GetComponent<FlyEnemy>();
                bee = script.bee;

                AudioSource.PlayClipAtPoint(slurp, transform.position, 1f);
                // Kill enemy and spawn ragdoll
                ragdollModel = Instantiate(hit.collider.transform.Find("model").gameObject, hit.collider.transform.position, hit.collider.transform.rotation);
                
                Destroy(hit.collider.gameObject);

                // Create hook effect
                currentHook = Instantiate(hookPrefab, shootPoint.position, Quaternion.identity);
                lr = currentHook.GetComponent<LineRenderer>();
                lr.SetPosition(0, shootPoint.position);
                lr.SetPosition(1, ragdollModel.transform.position);

                StartCoroutine(swallow());
                
            }

            if (hit.collider.CompareTag("GrappleTarget")) // Ensure your target has this tag
            {
                AudioSource.PlayClipAtPoint(slurp, transform.position, 1f);
                Debug.Log("hit");
                targetObject = hit.collider.transform;
                //if (Vector3.Distance(transform.position, targetObject.position) > 8f) return;

                if (currentHook != null)
                {
                    Destroy(currentHook); // Destroy existing hook if there is one
                }

                // Instantiate the hook and set it up
                currentHook = Instantiate(hookPrefab, shootPoint.position, Quaternion.identity);
                lr = currentHook.GetComponent<LineRenderer>();
                lr.SetPosition(0, shootPoint.position);
                lr.SetPosition(1, targetObject.position);

                float distanceY = Vector3.Distance(new Vector3(0f, transform.position.y, 0f), new Vector3(0f, targetObject.position.y, 0f));
                if (transform.position.y < targetObject.position.y) rb.velocity += new Vector3(0f, 1.5f * distanceY, 0f);

                // Start moving towards the target
                //StartCoroutine(MoveTowardsTarget());
            }
        }
    }

    IEnumerator MoveTowardsTarget()
    {
        float distance = Vector3.Distance(transform.position, targetObject.position);

        while (distance > 2f)
        {
            Tree.transform.Rotate(0.0f, 5f * Time.deltaTime, 0.0f);



            yield return null;
        }

    }
}