using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public HealthCounter healthCounter;

    void Start() {
        //Find GameObject named HealthCounter in the scene
        GameObject healthGO = GameObject.Find("HealthCounter");

        //Get the HealthCounter script component of healthGO
        healthCounter = healthGO.GetComponent<HealthCounter>();
    }

    void OnCollisionEnter( Collision coll ) {                             
        // Find out what hit this basket
        GameObject collidedWith = coll.gameObject;    
        
        if ( collidedWith.CompareTag("Blast") ) {
            //destroys collided object
            Destroy( collidedWith );

            //decrease health
            healthCounter.health -= 25;

            if ( healthCounter.health == 0 ) {
                
                SceneManager.LoadScene("Main-Prototype 1");

            }
        }

    }
}
