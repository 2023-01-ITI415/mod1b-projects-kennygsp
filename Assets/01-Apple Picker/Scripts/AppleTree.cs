using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleTree : MonoBehaviour
{
    //Compiler Attribute that tells Unity to place a header line above the following field in
    //the Inspector for this script
    [Header("Inscribed")]

    //Prefab for instantiating apples
    public GameObject applePrefab;

    //Speed at which the AppleTree moves
    public float speed = 1f;

    //Distance where AppleTree turns around
    public float leftAndRightEdge = 10f;

    //Chance that the AppleTree will change directions
    public float changeDirChance = 0.1f;

    //Seconds between Apple instantiations
    public float appleDropDelay = 1f;

    void Start () {
        //Start dropping apples function, waits 2 seconds/frames
        Invoke("DropApple", 2f);
    }

    void DropApple() {
        GameObject apple = Instantiate<GameObject>(applePrefab);
        apple.transform.position = transform.position;
        Invoke("DropApple", appleDropDelay);
    }

    void Update () {
        //Basic Movement
        //defines pos as the position of the appletree at this moment
        Vector3 pos = transform.position;

        //Movement is timed based
        pos.x += speed * Time.deltaTime;

        //moves appletree to a new position
        transform.position = pos;

        //Changing Direction
        if (pos.x < -leftAndRightEdge) {
            //moves right when it hits the wall
            speed = Mathf.Abs(speed);
        }
        else if (pos.x > leftAndRightEdge) {
            //moves left when it hits the wall
            speed = -Mathf.Abs(speed);
        }
        else if (Random.value < changeDirChance) {
            speed *= -1;
        }
    }

    void FixedUpdate () {
        //random direction changes are now time-based due to fixedupdate
        if(Random.value < changeDirChance) {
            speed *= -1;
        }
    }
}
