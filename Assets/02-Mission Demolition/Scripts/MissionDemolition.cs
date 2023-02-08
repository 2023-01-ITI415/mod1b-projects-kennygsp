using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameMode {
    idle,
    playing,
    levelEnd
}

public class MissionDemolition : MonoBehaviour
{
    static private MissionDemolition S;

    [Header("Inscribed")]
    public Text uitLevel; //The Level Text
    public Text uitShots; //The Shots Text
    public Vector3 castlePos; //place to put castles
    public GameObject[] castles; //array of castles

    [Header("Dynamic")]
    public int level; //current level
    public int levelMax; //number of levels
    public int shotsTaken; 
    public GameObject castle; //current castle
    public GameMode mode = GameMode.idle;
    public string showing = "Show Slingshot"; //followcam

    void Start() {
        S = this; //define the singleton

        level = 0;
        shotsTaken = 0;
        levelMax = castles.Length;
        StartLevel();
    }

    void StartLevel() {
        //get rid of the old castle if one exists
        if ( castle != null ) {
            Destroy(castle);
        }

        //Destroy old projectiles if they exist
        Projectile.DESTROY_PROJECTILES();

        //Instantiate the new castle
        castle = Instantiate<GameObject>(castles[level]);
        castle.transform.position = castlePos;

        //reset the goal
        Goal.goalMet = false;
        
        UpdateGUI();

        mode = GameMode.playing;
    }

    void UpdateGUI() {
        //show the data in the GUITexts
        uitLevel.text = "Level: " + (level+1) + " of " + levelMax;
        uitShots.text = "Shots Taken: " + shotsTaken;
    }

    void Update() {
        UpdateGUI();

        //check for level end
        if( (mode == GameMode.playing) && Goal.goalMet) {
            //change mode to stop checking for level end
            mode = GameMode.levelEnd;

            //start the next level in 2 seconds
            Invoke("NextLevel", 2f);
        }
    }

    void NextLevel() {
        level++;
        if(level == levelMax) {
            level = 0;
            shotsTaken = 0;
        }
        StartLevel();
    }

    //static method that allows code anywhere to increment shotsTaken
    static public void SHOTS_FIRED() {
        S.shotsTaken++;
    }

    //static method that allows code anywhere to get a reference to S.castle
    static public GameObject GET_CASTLE() {
        return S.castle;
    }
}
