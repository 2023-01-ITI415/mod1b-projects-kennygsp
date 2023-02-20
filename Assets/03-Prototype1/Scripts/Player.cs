using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    // Audio variables
    public AudioSource audioSource;
    public AudioSource audioSource2;

    public HealthCounter healthCounter;
    public BossCounter bossCounter;
    private CharacterController _characterController;

    void Start() {
        //Find GameObject named HealthCounter in the scene
        GameObject healthGO = GameObject.Find("HealthCounter");
        
        //Find GameObject named BossCounter in the scene
        GameObject bossGO = GameObject.Find("BossCounter");

        //Get the HealthCounter script component of healthGO
        healthCounter = healthGO.GetComponent<HealthCounter>();

        bossCounter = bossGO.GetComponent<BossCounter>();
    }

    void OnCollisionEnter( Collision coll ) {                             
        // Find out what hit this basket
        GameObject collidedWith = coll.gameObject;    
        
        if ( collidedWith.CompareTag("Blast") ) {
            //destroys collided object
            Destroy( collidedWith );

            audioSource2.Play();

            //decrease health
            healthCounter.health -= 25;

            if ( healthCounter.health == 0 ) {
                
                SceneManager.LoadScene("Main-Prototype 1");

            }
        }

        if ( collidedWith.CompareTag("UFO") ) {

            audioSource.Play();
            //decrease health
            bossCounter.bossHealth -= 25;

            if ( bossCounter.bossHealth == 0 ) {
                
                SceneManager.LoadScene("Main-Prototype 1");
            }
        }



    }
}
