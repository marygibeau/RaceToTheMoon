using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class reticleMovementScript : MonoBehaviour
{
    // Start is called before the first frame update

    // for target star logic, maintain list of stars that have been selected and stars that can be selected 
    // make sure new targetStar is in not found array before assigning

    // movement variables
    public float movementOffset = 0.1f;
    public cameraMovement camera;
    public GameObject backgroundImage;
    private bool reticleUp = false;
    private bool reticleDown = false;
    private bool reticleLeft = false;
    private bool reticleRight = false;

    // UI variables
    Text scoreUI;
    Text starText;
    GameObject blackBox;
    GameObject textBox;
    private int currentScore = 0;
    public int scoreIncrement = 1000;

    // star clicking variables
    bool canClick = true;
    public float coolDown = 1.0f;
    TargetStar targetScript;

    // audio variables
    AudioSource selectionSound;

    // level management variables
    LevelManager lvlr;

    void Start()
    {
        scoreUI = GameObject.Find("ScoreText").GetComponent<Text>();
        scoreUI.text = "Score: 00000";
        blackBox = this.transform.GetChild(0).gameObject;
        starText = this.transform.GetChild(1).gameObject.GetComponent<Text>();
        textBox = this.transform.GetChild(2).gameObject;
        lvlr = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        targetScript = GameObject.Find("TargetStarHandler").GetComponent<TargetStar>();
        selectionSound = gameObject.GetComponent<AudioSource>();
        starsFound = 0;
        hideBox();
    }

    // Update is called once per frame
    void Update()
    {
        // reticle movement
        if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && !reticleUp)
        {
            this.transform.Translate(Vector2.up * movementOffset);
        }
        if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && !reticleDown)
        {
            this.transform.Translate(Vector2.down * movementOffset);
        }
        if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && !reticleLeft)
        {
            this.transform.Translate(Vector2.left * movementOffset);
        }
        if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && !reticleRight)
        {
            this.transform.Translate(Vector2.right * movementOffset);
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

        // for target star logic, change <starText.text != ""> to <starText.text == targetStarName>
        // logic for star being clicked
        if (Input.GetKeyUp(KeyCode.Return) && canClick && starText.text == targetScript.GetTarget())
        {
            // for target star logic, maintain list of stars that have been selected and stars that can be selected, make sure starName is not contained in found stars array before incrementing score
            canClick = false;
            increaseScore(scoreIncrement);
            Invoke("CooledDown", coolDown);
            UpdateTargetStarDebug();
        }

        //testing increaseScore
        if (Input.GetKeyDown(KeyCode.Q))
        {
            increaseScore(scoreIncrement);
        }

        // testing load with final score
        if (Input.GetKeyDown(KeyCode.E))
        {
            lvlr.LoadNextLevelWithScoreandStars(currentScore, targetScript.GetNumberOfStarsFound());
        }

        // cycle target star
        if (Input.GetKeyDown(KeyCode.T))
        {
            UpdateTargetStarDebug();
        }
    }

    // checks if reticle has collided with a trigger
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
            default:
                showBox(other.gameObject.name);
                break;
        }
    }

    // checks if reticle is no longer colliding with a trigger
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
            default:
                hideBox();
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

    // increases the score shown by an amount passed
    public void increaseScore(int amount)
    {
        currentScore += amount;
        // if score is 4 digits, keeps the leading zero, else just show the 5 digit score
        updateScoreText(currentScore);
        //play the sound effect for selecting the correct star
        selectionSound.Play();
    }

    public void updateScoreText(int score)
    {
        if (currentScore < 10000)
        {
            scoreUI.text = "Score: " + ("0" + currentScore);
        }
        else scoreUI.text = "Score: " + currentScore.ToString();
    }

    void CooledDown()
    {
        canClick = true;
    }

    public int getScore()
    {
        return currentScore;
    }

    public int getStars()
    {
        return targetScript.GetNumberOfStarsFound();
    }

    void UpdateTargetStarDebug()
    {
        UpdateTargetStar();
        Debug.Log("New target star = " + targetScript.GetTarget());
        Debug.Log("Found stars include: ");
        foreach (string star in targetScript.GetNamesOfStarsFound())
        {
            Debug.Log("  - " + star);
        }
    }

    void UpdateTargetStar()
    {
        targetScript.UpdateTarget();
    }

}
