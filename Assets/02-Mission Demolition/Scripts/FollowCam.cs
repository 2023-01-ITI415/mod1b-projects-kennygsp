using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    //POI stands for the Point of Interest that the camera should follow
    static public GameObject POI;

    [Header("Inscribed")]
    public float easing = 0.05f;
    public Vector2 minXY = Vector2.zero;

    [Header("Dynamic")]
    public float camZ; //The desired R pos of the camera

    void Awake() {
        camZ = this.transform.position.z;
    }

    void FixedUpdate() {
        Vector3 destination = Vector3.zero;

        if (POI != null) {
            
            //if the POI has a Rigidbody check to see if it is sleeping
            Rigidbody poiRigid = POI.GetComponent<Rigidbody>();
            if ( (poiRigid != null) && poiRigid.IsSleeping() ) {
                POI = null;
            }
        }

        if (POI != null) {
            destination = POI.transform.position;
        }

        //Limit the minimum values of destination.x & destination.y
        destination.x = Mathf.Max( minXY.x, destination.x );
        destination.y = Mathf.Max( minXY.y, destination.y );

        //Interpolate from the current Camera position toward destination
        destination = Vector3.Lerp(transform.position, destination, easing);

        //Force destination.z to be camZ to keep the camera far enough away
        destination.z = camZ;

        //Set the camera to the destination
        transform.position = destination;

        //Set the orthographicSize of the Camera to keep the Ground in view
        Camera.main.orthographicSize = destination.y + 10;
    }
}