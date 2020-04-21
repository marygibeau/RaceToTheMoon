using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class TutorialReticleScript : MonoBehaviour
{
    // movement variables
    public float movementOffset = 0.1f;
    public cameraMovement cameraMovementScript;
    public Camera mainCamera;
    public GameObject backgroundImage;
    private bool reticleUp = false;
    private bool reticleDown = false;
    private bool reticleLeft = false;
    private bool reticleRight = false;
    private bool canMove = true;

    // UI variables
    Text scoreUI;
    Text starText;
    GameObject blackBox;
    GameObject textBox;
    GameObject continueBox;
    private int currentScore = 0;
    public int scoreIncrement = 1000;

    // star clicking variables
    bool canClick = true;
    public float coolDown = 1.0f;


    // audio variables
    AudioSource selectionSound;
    AudioSource incorrectSound;
    AudioSource hintSound;
    AudioClip farSound;
    AudioClip midSound;
    AudioClip closeSound;
    AudioClip rapidSound;

    // level management variables
    LevelManager lvlr;
    public Text tutorialPanelText;
    bool gameOver;
    TimerScript timer;

    // tutorial variables
    int tutorialStage;
    GameObject TargetStarText;
    int movements = 0;
    public VideoPlayer video;
    GameObject alpheratz;
    GameObject navi;
    string[] instructions = {"Move the reticle using wasd or arrow keys.",
                             "We’ve built some star tracking technology into the ship. The mission critical stars will have a circle around them.",
                             "Move the reticle over a star to see its name.",
                             "In the upper right hand corner is the name of the star you’re looking for",
                             "If the name of the star matches the name of the target star, press enter to earn points.",
                             "Some stars are outside of our view. To move the viewport, move your reticle in the desired direction until it collides with the box.",
                             "If you’re having trouble finding a star, first the circle around the star will expand and shrink to draw your attention.",
                             "Next our audio tracking system will play beeps that get faster as you get closer to the target star and a special sound will play when you hover over the correct star.",
                             "Finally, arrows will appear to help guide you in the direction of very difficult stars.",
                             "Because this is a critical mission, you’ll only have a certain amount of time.",
                             "Try to find as many stars as you can within the time limit to recalibrate the navigation system."};

    // Start is called before the first frame update
    void Start()
    {
        scoreUI = GameObject.Find("ScoreText").GetComponent<Text>();
        scoreUI.text = "Score: 01000";
        scoreUI.gameObject.SetActive(false);
        blackBox = this.transform.GetChild(0).gameObject;
        starText = this.transform.GetChild(1).gameObject.GetComponent<Text>();
        textBox = this.transform.GetChild(2).gameObject;

        lvlr = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        selectionSound = gameObject.GetComponents<AudioSource>()[1];
        hintSound = gameObject.GetComponents<AudioSource>()[0];
        incorrectSound = gameObject.GetComponents<AudioSource>()[2];
        closeSound = (AudioClip)Resources.Load("sounds/boopClose");
        midSound = (AudioClip)Resources.Load("sounds/boopMid");
        farSound = (AudioClip)Resources.Load("sounds/boopFar");
        rapidSound = (AudioClip)Resources.Load("sounds/boopRapid");
        video = GameObject.Find("Video Player").GetComponent<VideoPlayer>();
        tutorialStage = 0;
        tutorialPanelText = GameObject.Find("TutorialText").GetComponent<Text>();
        continueBox = GameObject.Find("continueText");
        tutorialPanelText.text = instructions[tutorialStage];
        gameOver = false;
        timer = GameObject.Find("Timer").GetComponent<TimerScript>();
        timer.gameObject.SetActive(false);
        TargetStarText = GameObject.Find("TargetText");
        TargetStarText.GetComponent<Text>().text = "Target: Alpheratz";
        TargetStarText.GetComponent<Text>().enabled = false;

        alpheratz = GameObject.Find("Alpheratz");
        navi = GameObject.Find("Navi");

        alpheratz.GetComponent<Renderer>().enabled = false;
        navi.GetComponent<Renderer>().enabled = false;

        alpheratz.transform.GetChild(0).gameObject.GetComponent<Renderer>().enabled = false;
        navi.transform.GetChild(0).gameObject.GetComponent<Renderer>().enabled = false;

        hideBox();
    }

    // Update is called once per frame
    void Update()
    {
        // reticle movement
        if (canMove)
        {
            if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && !reticleUp)
            {
                if (movements < 61 && tutorialStage < 2 || tutorialStage == 3) { movements++; }
                this.transform.Translate(Vector2.up * movementOffset);
            }
            if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && !reticleDown)
            {
                if (movements < 61 && tutorialStage < 2 || tutorialStage == 3) { movements++; }
                this.transform.Translate(Vector2.down * movementOffset);
            }
            if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && !reticleLeft)
            {
                if (movements < 61 && tutorialStage < 2 || tutorialStage == 3) { movements++; }
                this.transform.Translate(Vector2.left * movementOffset);
            }
            if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && !reticleRight)
            {
                if (movements < 61 && tutorialStage < 2 || tutorialStage == 3) { movements++; }
                this.transform.Translate(Vector2.right * movementOffset);
            }
        }
        // debugging printout for movement variable
        if (movements < 61 && movements > 0) { Debug.Log(movements); }

        // advance tutorial stage for 0 and 6
        if (movements >= 60 && (tutorialStage == 0 || tutorialStage == 5))
        {
            AdvanceTutorial();
            movements = 0;
        }

        // advance tutorial debug button
        if (Input.GetKeyUp(KeyCode.E) || (Input.GetKeyUp(KeyCode.Return)))
        {
            AdvanceTutorial();
        }

        // camera movement
        if (reticleUp && tutorialStage >= 5 && canMove)
        {
            if (movements < 65 && tutorialStage == 5) { movements++; }
            cameraMovementScript.moveUp();
        }
        if (reticleDown && tutorialStage >= 5 && canMove)
        {
            if (movements < 65 && tutorialStage == 5) { movements++; }
            cameraMovementScript.moveDown();
        }
        if (reticleLeft && tutorialStage >= 5 && canMove)
        {
            if (movements < 65 && tutorialStage == 5) { movements++; }
            cameraMovementScript.moveLeft();
        }
        if (reticleRight && tutorialStage >= 5 && canMove)
        {
            if (movements < 65 && tutorialStage == 5) { movements++; }
            cameraMovementScript.moveRight();
        }

        // logic for star being clicked
        if (Input.GetKeyUp(KeyCode.Return) && canClick && starText.text == "Alpheratz" && !gameOver && tutorialStage >= 4)
        {
            canClick = false;
            Invoke("CooledDown", coolDown);
            ChangeTargetStarColor();
            if (tutorialStage == 4) { AdvanceTutorial(); }
        }

        //logic for clicking a star that is not the target to play sound effect
        if (Input.GetKeyUp(KeyCode.Return) && canClick && starText.text != "Alpheratz" && starText.text != "" && !gameOver)
        {
            canClick = false;
            Invoke("CooledDown", coolDown);
            incorrectSound.Play();
        }

    }

    // checks if reticle has collided with a trigger
    void OnTriggerEnter2D(Collider2D other)
    {
        // turns on movement directions
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

    // shows box by reticle star box
    public void showBox(string star)
    {
        // Debug.Log(star);
        if (star != "Main Camera" && star != "Button" && !gameOver)
        {
            textBox.gameObject.SetActive(true);
            
            blackBox.gameObject.SetActive(true);
            starText.text = star;
        }
        if (tutorialStage == 2)
        {
            AdvanceTutorial();
        }
    }

    // hides box by reticle
    public void hideBox()
    {
        textBox.gameObject.SetActive(false);
        blackBox.gameObject.SetActive(false);
        starText.text = "";
    }

    // helper function for star click
    void CooledDown()
    {
        canClick = true;
    }

    // changes color of target star ring to green
    void ChangeTargetStarColor()
    {
        GameObject targetStarRing = GameObject.Find("Alpheratz").transform.GetChild(0).gameObject;
        Debug.Log("targetStarRing name: " + targetStarRing.name);
        targetStarRing.GetComponent<SpriteRenderer>().color = new Color(0.1727581f, 0.945098f, 0.1215686f, 1);
    }

    // Advances the Tutorial to the next stage and implements that stage's logic
    void AdvanceTutorial()
    {
        // had to use E button on stages: 1->2, 3->4, 6+

        tutorialStage++;
        if (tutorialStage >= 11) // loads game
        {
            // start game button
            SceneManager.LoadScene("Game");
        }
        else
        {
            tutorialPanelText.text = instructions[tutorialStage];
        }
        
        Debug.Log("Tutorial Advanced to Stage: " + tutorialStage);

        if (tutorialStage == 1) // shows stars
        {
            alpheratz.GetComponent<Renderer>().enabled = true;
            navi.GetComponent<Renderer>().enabled = true;
            alpheratz.transform.GetChild(0).gameObject.GetComponent<Renderer>().enabled = true;
            navi.transform.GetChild(0).gameObject.GetComponent<Renderer>().enabled = true;
        }
        else if (tutorialStage == 3) // shows target star
        {

            TargetStarText.GetComponent<Text>().enabled = true;

        }
        else if (tutorialStage == 5) // shows score and changes target star
        {
            scoreUI.gameObject.SetActive(true);
            TargetStarText.GetComponent<Text>().text = "Target: Well Done!";
        }
        else if (tutorialStage == 6) // hides game components and shows first hint video
        {
            canMove = false;
            hideBox();
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            scoreUI.gameObject.SetActive(false);
            GameObject.Find("TargetText").GetComponent<Text>().enabled = false;
            GameObject.Find("continueBox").SetActive(false);
            GameObject.Find("continuePanel").SetActive(false);
            GameObject.Find("continueText").SetActive(false);
            video.clip = (VideoClip)Resources.Load("hintVideos/hint1");
            video.Play();
        }
        else if (tutorialStage == 7) // shows second hint video
        {
            video.clip = (VideoClip)Resources.Load("hintVideos/hint2");
            video.Play();
        }
        else if (tutorialStage == 8) // shows third hint video
        {
            video.clip = (VideoClip)Resources.Load("hintVideos/hint3");
            video.Play();
        }
        else if (tutorialStage == 9) // shows game components and hides videos, timed mission
        {
            canMove = true;
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            scoreUI.gameObject.SetActive(false);
            GameObject.Find("TargetText").GetComponent<Text>().enabled = false;
            GameObject.Find("TargetText").GetComponent<Text>().enabled = false;
            GameObject.Find("continueBox").SetActive(true);
            GameObject.Find("continuePanel").SetActive(true);
            GameObject.Find("continueText").SetActive(true);
            video.enabled = false;
            // TODO: show timer
        }
        else if (tutorialStage == 10) // show transition to game
        {
            // TODO
        }
    }
}
