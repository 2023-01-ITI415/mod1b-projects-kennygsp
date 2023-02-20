using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossCounter : MonoBehaviour
{
    [Header("Dynamic")]

    public int bossHealth = 100;
    private Text uiText;

    void Start() {

        uiText = GetComponent<Text>();

    }

    void Update() {
        uiText.text = bossHealth.ToString( "Boss Health: #,0" );
    }

}
