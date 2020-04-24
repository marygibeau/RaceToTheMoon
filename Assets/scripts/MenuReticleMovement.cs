using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuReticleMovement : MonoBehaviour
{
    // movement variables
    float movementOffset = 4.0f;
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
    public GameObject button;


    // Audio Variables
    AudioSource hintSound;
    AudioClip farSound;
    AudioSource selectionSound;

    // level management variables
    LevelManager lvlr;

    // Cinematic variables
  //  public GameObject videoPlayer;
  //  public int timeToStop;

    // Start is called before the first frame update
    void Start()
    {
        blackBox = this.transform.GetChild(0).gameObject;
        starText = this.transform.GetChild(1).gameObject.GetComponent<Text>();
        textBox = this.transform.GetChild(2).gameObject;
        // videoPlayer.SetActive(false);
        //selectionSound = gameObject.GetComponents<AudioSource>()[1];
        //hintSound = gameObject.GetComponents<AudioSource>()[0];
        //farSound = (AudioClip)Resources.Load("sounds/boopFar");
        lvlr = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        showBox(initialHint);
    }

    // Update is called once per frame
    void Update()
    {
        if (hasMoved && (!onPlayButton || !onRestartButton))
        {
            button.GetComponent<Image>().color = Color.white;
            hideBox();
        }

        if (onPlayButton)
        { 
            button.GetComponent<Image>().color = Color.blue;
            showBox(PlayButtonHint);
        }

        if (onRestartButton)
        {
            button.GetComponent<Image>().color = Color.blue;
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
            print("Cinematic 1 to play here.");

            lvlr.LoadLevel("Cinematic_1");
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
            this.transform.Translate(Vector2.right * movementOffset*Time.deltaTime);
        }
    }

    public void moveLeft()
    {
        if (!leftBoundary)
        {
            hasMoved = true;
            this.transform.Translate(Vector2.left * movementOffset*Time.deltaTime);
        }
    }
    public void moveUp()
    {
        if (!upBoundary)
        {
            hasMoved = true;
            this.transform.Translate(Vector2.up * movementOffset*Time.deltaTime);
        }
    }

    public void moveDown()
    {
        if (!downBoundary)
        {
            hasMoved = true;
            this.transform.Translate(Vector2.down * movementOffset*Time.deltaTime);
        }
    }
}
