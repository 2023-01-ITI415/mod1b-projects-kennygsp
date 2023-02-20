using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // Pausing game variable
    public static bool gameIsPaused;
    public GameObject PauseMenu;

    public float speed = 0;
    public float jumpAmount = 10;
    public float gravityScale = 5;

    private Rigidbody rb;
    private float movementX;
    private float movementY;

    //Jump variables
    private float lastJumpTime = 0f;
    private float jumpDelay = 2f; // 2 second daily
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void Update()
    {
        // gets the current time
        float currentTime = Time.time;

        // check to see if space is pressed and the jump delay time
        if (Input.GetKeyDown(KeyCode.Space) && (currentTime - lastJumpTime) >= jumpDelay)
        {
            // add force to the rigidbody to jump
            rb.AddForce(Vector2.up * jumpAmount, ForceMode.Impulse);

            // update the last jump time to the current time
            lastJumpTime = currentTime;
        }

        // Reset Scene
        if ( Input.GetKeyDown(KeyCode.R) ) {

                SceneManager.LoadScene("Main-Prototype 1");

        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(gameIsPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp")) 
        {
            other.gameObject.SetActive(false);

        }
    }

    public void PauseGame()
    {
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void ResumeGame()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("SceneMain");
    }
}