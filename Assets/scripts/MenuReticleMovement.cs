using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuReticleMovement : MonoBehaviour
{
    // movement variables
    public float movementOffset = 0.1f;
    private bool upBoundary = false;
    private bool downBoundary = false;
    private bool leftBoundary = false;
    private bool rightBoundary = false;
    private bool hasMoved = false;
    private bool onPlayButton = false;
    private bool onRestartButton = false;

    // UI variables
    Text starText;
    GameObject blackBox;
    GameObject textBox;
    string PlayButtonHint = "Press Enter to Play!";
    string RestartButtonHint = "Press Enter to Restart the Game";
    string initialHint = "Use WASD or Arrow Keys to Move";

    // level management variables
    LevelManager lvlr;

    // Start is called before the first frame update
    void Start()
    {
        blackBox = this.transform.GetChild(0).gameObject;
        starText = this.transform.GetChild(1).gameObject.GetComponent<Text>();
        textBox = this.transform.GetChild(2).gameObject;
        lvlr = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        showBox(initialHint);
    }

    // Update is called once per frame
    void Update()
    {
        if (hasMoved && !onPlayButton)
        {
            hideBox();
        }

        if (onPlayButton)
        {
            showBox(PlayButtonHint);
        }

        if (onRestartButton)
        {
            showBox(RestartButtonHint);
        }

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            moveUp();
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            moveDown();
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            moveLeft();
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            moveRight();
        }

        if (Input.GetKeyUp(KeyCode.Return) && starText.text == PlayButtonHint)
        {
            lvlr.LoadNextLevel();
        }

        if (Input.GetKeyUp(KeyCode.Return) && starText.text == RestartButtonHint)
        {
            lvlr.LoadLevel("MainMenu");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // turns on movement directions
        Debug.Log("Entered: " + other.gameObject.name);
        switch (other.gameObject.name)
        {
            case "right":
                rightBoundary = true;
                break;
            case "left":
                leftBoundary = true;
                break;
            case "up":
                upBoundary = true;
                break;
            case "down":
                downBoundary = true;
                break;
            case "PlayButton":
                onPlayButton = true;
                break;
            case "RestartButton":
                onRestartButton = true;
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
                rightBoundary = false;
                break;
            case "left":
                leftBoundary = false;
                break;
            case "up":
                upBoundary = false;
                break;
            case "down":
                downBoundary = false;
                break;
            case "PlayButton":
                onPlayButton = false;
                break;
            case "RestartButton":
                onRestartButton = false;
                break;
        }
    }

    public void showBox(string star)
    {
        if (star != "Main Camera")
        {
            textBox.gameObject.SetActive(true);
            blackBox.gameObject.SetActive(true);
            starText.text = star;
        }
    }

    public void hideBox()
    {
        textBox.gameObject.SetActive(false);
        blackBox.gameObject.SetActive(false);
        starText.text = "";
    }

    public void moveRight()
    {
        if (!rightBoundary)
        {
            hasMoved = true;
            this.transform.Translate(Vector2.right * movementOffset);
        }
    }

    public void moveLeft()
    {
        if (!leftBoundary)
        {
            hasMoved = true;
            this.transform.Translate(Vector2.left * movementOffset);
        }
    }
    public void moveUp()
    {
        if (!upBoundary)
        {
            hasMoved = true;
            this.transform.Translate(Vector2.up * movementOffset);
        }
    }

    public void moveDown()
    {
        if (!downBoundary)
        {
            hasMoved = true;
            this.transform.Translate(Vector2.down * movementOffset);
        }
    }
}
