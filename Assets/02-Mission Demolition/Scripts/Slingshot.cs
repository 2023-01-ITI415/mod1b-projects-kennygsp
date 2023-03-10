using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    [Header("Inscribed")]
    public GameObject projectilePrefab;
    public float velocityMult = 10f;
    public GameObject projLinePrefab;

    [Header("Dynamic")]
    public GameObject launchPoint;
    public Vector3 launchPos;
    public GameObject projectile;
    public bool aimingMode;

    void Awake() {

        //searches for a child of Slingshot named LaunchPoint and returns its Transform
        Transform launchPointTrans = transform.Find("LaunchPoint");

        //Gets the GameObject associated with that Transform and assigns it to GameObject launchPoint
        launchPoint = launchPointTrans.gameObject;
        
        //tells the game whether or not to ignore them
        launchPoint.SetActive(false);

        launchPos = launchPointTrans.position;
    }

    void OnMouseEnter() {
        //print("Slingshot:OnMouseEnter()");
        launchPoint.SetActive(true);
    }

    void OnMouseExit() {
        //print("Slingshot:OnMouseExit()");
        launchPoint.SetActive(false);
    }

    void OnMouseDown() {

        //The player has presed the mouse button while over Slingshot
        aimingMode = true;

        //Instantiate a Projectile
        projectile = Instantiate( projectilePrefab ) as GameObject;

        //Start it at the launchPoint
        projectile.transform.position = launchPos;

        //Set it to isKinematic for now
        projectile.GetComponent<Rigidbody>().isKinematic = true; 
    }

    void Update() {
        //If slingshot is not in aimingMode, dont run this code
        if (!aimingMode) return;

        //Get the current mouse position in 2D screen coordinates
        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        //Find the detla from the launchPos to the mousePos3D
        Vector3 mouseDelta = mousePos3D -launchPos;
        //Limit mouseDelta to the radius of the Slingshot SphereCollider
        float maxMagnitude = this.GetComponent<SphereCollider>().radius;
        if (mouseDelta.magnitude > maxMagnitude) {
            mouseDelta.Normalize();
            mouseDelta *= maxMagnitude;
        }

        //Move the projectile to this new position
        Vector3 projPos = launchPos + mouseDelta;
        projectile.transform.position = projPos;

        if( Input.GetMouseButtonUp(0) ) {
            aimingMode = false;
            Rigidbody projRB = projectile.GetComponent<Rigidbody>();
            projRB.isKinematic = false;
            projRB.collisionDetectionMode = CollisionDetectionMode.Continuous;
            projRB.velocity = -mouseDelta * velocityMult;

            FollowCam.SWITCH_VIEW(FollowCam.eView.slingshot);

            FollowCam.POI = projectile; //Set the Main Camera POI

            //Add a ProjectileLine to the Projectile
            Instantiate<GameObject>(projLinePrefab, projectile.transform);
            projectile = null;
            MissionDemolition.SHOT_FIRED();
        }
    }
}
