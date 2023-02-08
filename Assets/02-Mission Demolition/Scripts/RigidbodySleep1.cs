using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent( typeof(Rigidbody) )]

public class RigidbodySleep1 : MonoBehaviour
{
    private int sleepCountdown = 4;

    private Rigidbody rigid;

    void Awake() {
        rigid = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        if ( sleepCountdown > 0 ) {
            rigid.Sleep();
            sleepCountdown--;
        }
    }
}
