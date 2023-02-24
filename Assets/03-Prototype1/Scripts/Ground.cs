using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    void OnCollisionEnter( Collision coll ) {                             
        // Find out what hit this basket
        GameObject collidedWith = coll.gameObject;    
        
        if ( collidedWith.CompareTag("Blast") ) {
           //destroys collided object
            Destroy( collidedWith );

        }
    }
}
