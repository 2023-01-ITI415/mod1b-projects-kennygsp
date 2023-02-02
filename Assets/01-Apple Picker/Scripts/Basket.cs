using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : MonoBehaviour
{
    public ScoreCounter scoreCounter;

    void Start() {
        //Find a GameObject named ScoreCounter in the Scene Hierarchy
        GameObject scoreGO = GameObject.Find( "ScoreCounter" );

        //Get the ScoreCounter script component of scoreGO
        scoreCounter = scoreGO.GetComponent<ScoreCounter>();
    }
    // Update is called once per frame
    void Update()
    {
        //get the current screen position of the mouse from input
        Vector3 mousePos2D = Input.mousePosition;

        //The camera's Z position sets how far to push the mouse into 3D
        mousePos2D.z = -Camera.main.transform.position.z;

        //Convert the point from 2D screen space into 3D game world space
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        //Move the x position of this Basket to the x position of the mouse
        Vector3 pos = this.transform.position;
        pos.x = mousePos3D.x;
        this.transform.position = pos;
    }
    
    void OnCollisionEnter( Collision coll ) {                             
        // Find out what hit this basket
        GameObject collidedWith = coll.gameObject;                       
        if ( collidedWith.CompareTag("Apple") ) {
           //destroys collided object
            Destroy( collidedWith );

            //increase the score
            scoreCounter.score += 100;
            HighScore.TRY_SET_HIGH_SCORE( scoreCounter.score );
        }
    }
}