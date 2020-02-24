using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class reticleMovementScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float offset = 0.1f;
    public cameraMovement camera;
    public GameObject backgroundImage;
    public Text scoreUI;

    private int currentScore = 0;

    private bool reticleUp = false;
    private bool reticleDown = false;
    private bool reticleLeft = false;
    private bool reticleRight = false;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // reticle movement
        if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && !reticleUp)
        {
            this.transform.Translate(Vector2.up * offset);
        }
        if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && !reticleDown)
        {
            this.transform.Translate(Vector2.down * offset);
        }
        if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && !reticleLeft)
        {
            this.transform.Translate(Vector2.left * offset);
        }
        if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && !reticleRight)
        {
            this.transform.Translate(Vector2.right * offset);
        }

        // camera movement
        if (reticleUp)
        {
            camera.moveUp();
        }
        if (reticleDown)
        {
            camera.moveDown();
        }
        if (reticleLeft)
        {
            camera.moveLeft();
        }
        if (reticleRight)
        {
            camera.moveRight();
        }

        //testing increaseScore
        if (Input.GetKeyDown(KeyCode.Q)) {
            increaseScore(1000);
        }
    }

    // checks if reticle has reached edge
    void OnTriggerEnter2D(Collider2D other)
    {
        // turns on movement directions
        Debug.Log("Entered: " + other.gameObject.name);
        switch (other.gameObject.name)
        {
            case "right":
                reticleRight = true;
                break;
            case "left":
                reticleLeft = true;
                break;
            case "up":
                reticleUp = true;
                break;
            case "down":
                reticleDown = true;
                break;
            
        }
    }

    // checks if reticle is no longer touching the edge
    void OnTriggerExit2D(Collider2D other)
    {
        // turns off movement directions
        Debug.Log("Exited: " + other.gameObject.name);
        switch (other.gameObject.name)
        {
            case "right":
                reticleRight = false;
                break;
            case "left":
                reticleLeft = false;
                break;
            case "up":
                reticleUp = false;
                break;
            case "down":
                reticleDown = false;
                break;
            
        }
    }


    // increases the score shown by an amount passed
    private void increaseScore(int amount) {
        currentScore += amount;
        // if score is 4 digits, keeps the leading zero, else just show the 5 digit score
        if(currentScore < 10000) 
        {
            scoreUI.text = ("0" + currentScore);
        } else scoreUI.text = currentScore.ToString();
    }

}
