using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
     private float collisionStartTime = 99999999f; // Variable to store the start time of collision
     public float setContactDuration;

     public Rigidbody platform;

    // Start is called before the first frame update
    void Start()
    {
        platform = GetComponent<Rigidbody>();
       
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - collisionStartTime > setContactDuration)
            platform.constraints = RigidbodyConstraints.None;
    }

    void OnCollisionEnter(Collision collision) {
      // Record the time when the collision starts
      collisionStartTime = Time.time;
    }

    void OnCollisionExit(Collision collision) {
      // Reset the collision start time
      collisionStartTime = 999999999f;
    }   

}
