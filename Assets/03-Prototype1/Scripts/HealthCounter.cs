using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthCounter : MonoBehaviour
{
    [Header("Dynamic")]

    public int health = 100;
    private Text uiText;

    void Start() {

        uiText = GetComponent<Text>();

    }

    void Update() {
        uiText.text = health.ToString( "Health: #,0" );
    }

}
