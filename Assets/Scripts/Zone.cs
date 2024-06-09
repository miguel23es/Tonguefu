using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        //Debug.Log("enter");
        if (other.gameObject.CompareTag("Player")) {
            foreach (Transform childTransform in this.transform) {
                FlyEnemy flyEnemyComponent = childTransform.GetComponent<FlyEnemy>();

                if (flyEnemyComponent != null) {
                    flyEnemyComponent.inZone = true;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        //Debug.Log("exit");
        if (other.gameObject.CompareTag("Player")) {
            foreach (Transform childTransform in this.transform) {
                FlyEnemy flyEnemyComponent = childTransform.GetComponent<FlyEnemy>();

                if (flyEnemyComponent != null) {
                    flyEnemyComponent.inZone = false;
                }
            }
        }
    }
}
