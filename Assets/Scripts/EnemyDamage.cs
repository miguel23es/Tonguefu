
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{

    public PlayerHealth playerHealth;
    public float damage = 1;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GameObject.Find("Frog").GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Frog")
        {
            playerHealth.TakeDamage(damage);
            Debug.Log("Hit");
        }
            
    }
}
