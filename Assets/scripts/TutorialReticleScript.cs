using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class TutorialReticleScript : MonoBehaviour
{
    // movement variables
    float movementOffset = 4.0f;
    public CameraMovement cameraMovementScript;
    public Camera mainCamera;
    public GameObject backgroundImage;
    public GameObject button;
    private bool crosshairUp = false;
    private bool crosshairDown = false;
    private bool crosshairLeft = false;
    private bool crosshairRight = false;
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
    TimerScript timer;
    public GameObject transitionPanel;
    public GameObject finalPanel;
    public Text transitionPanelText;
    bool startButtonHovered;
    bool reviewButtonHovered;
    bool goButtonHovered;
    string introText = "If you would like a refresher on your star tracking press the review button. If not, press start to skip this training review.";
    string endText = "You have successfully completed your training review. Press GO to begin system calibration.";


    // tutorial variables
    int tutorialStage;
    Text tutorialPanelText;
    GameObject tutorialPanel;
    GameObject TargetStarText;
    int movements = 0;
    public VideoPlayer video;
    public VideoPlayer transVideo;
    GameObject alpheratz;
    GameObject navi;
    float enterButtonPressed;
    bool advancingTutorial = false;
    private bool canSkip = false;
    public GameObject reviewButton;
    public GameObject goButton;
    public GameObject startButton;


    string[] instructions = {"Move the crosshair using the joystick.",
                             "We’ve built some star tracking technology into the ship. The mission critical stars will have a circle around them.",
                             "In the upper right hand corner is the name of the star you’re looking for",
                             "Move the crosshair over a star to see its name.",
                             "If the name of the star matches the name of the target star, press the joystick button to earn points.",
                             "Some stars are outside of our view. To move the viewport, move your crosshair in the desired direction until it collides with the box.",
                             "Excellent work! To help you track down difficult stars, we've built some star tracking technology into the ship.",
                             "First the circle around the star will expand and shrink to draw your attention.",
                             "Next our audio tracking system will play beeps that get faster as you get closer to the target star and a special sound will play when you hover over the correct star.",
                             "Finally, arrows will appear to help guide you in the direction of very difficult stars.",
                             "Because this is a critical mission, you’ll only have a certain amount of time.",
                             "Try to find as many stars as you can within the time limit to recalibrate the navigation system."};

    // Start is called before the first frame update
    void Start()
    {
        // set score and hide
        scoreUI = GameObject.Find("ScoreText").GetComponent<Text>();
        scoreUI.text = "Score: 01000";
        // get star box components and hide
        blackBox = this.transform.GetChild(0).gameObject;
        starText = this.transform.GetChild(1).gameObject.GetComponent<Text>();
        textBox = this.transform.GetChild(2).gameObject;
        // level manager
        lvlr = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        // sound variables initialization
        selectionSound = gameObject.GetComponents<AudioSource>()[1];
        hintSound = gameObject.GetComponents<AudioSource>()[0];
        incorrectSound = gameObject.GetComponents<AudioSource>()[2];
        closeSound = (AudioClip)Resources.Load("sounds/boopClose");
        midSound = (AudioClip)Resources.Load("sounds/boopMid");
        farSound = (AudioClip)Resources.Load("sounds/boopFar");
        rapidSound = (AudioClip)Resources.Load("sounds/boopRapid");
        // video set up
        video = GameObject.Find("Video Player").GetComponent<VideoPlayer>();
        // tutorial setup
        tutorialStage = -1;
        tutorialPanel = GameObject.Find("Tutorial Box");
        tutorialPanelText = GameObject.Find("TutorialText").GetComponent<Text>();
        transitionPanelText.text = introText;
        continueBox = GameObject.Find("continueText");
        goButtonHovered = false;
        startButtonHovered = false;
        reviewButtonHovered = false;
        goButton.SetActive(false);
        // timer set up and hide
        timer = GameObject.Find("Timer").GetComponent<TimerScript>();
        // target star set up and hide
        TargetStarText = GameObject.Find("TargetText");
        TargetStarText.GetComponent<Text>().text = "Target: Alpheratz";
        // star set up and hide
        alpheratz = GameObject.Find("Alpheratz");
        navi = GameObject.Find("Navi");
        // hide game objects (not crosshair) and skip box
        HideGameComponents();
        HideSkipBox();
    }

    // puts tutorial in beginning state
    void StartTutorial()
    {
        transitionPanel.gameObject.SetActive(false);
        finalPanel.gameObject.SetActive(false);
        startButton.SetActive(false);
        reviewButton.SetActive(false);
        tutorialStage = 0;
        tutorialPanel.gameObject.SetActive(true);
        tutorialPanelText.text = instructions[tutorialStage];
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        GameObject.Find("GameUI_TargetBox").GetComponent<SpriteRenderer>().enabled = true;
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        // crosshair movement
        if (canMove)
        {
            if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && !crosshairUp)
            {
                if (movements < 61 && tutorialStage == 0) { movements++; }
                this.transform.Translate(Vector2.up * movementOffset * Time.deltaTime);
            }
            if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && !crosshairDown)
            {
                if (movements < 61 && tutorialStage == 0) { movements++; }
                this.transform.Translate(Vector2.down * movementOffset * Time.deltaTime);
            }
            if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && !crosshairLeft)
            {
                if (movements < 61 && tutorialStage == 0) { movements++; }
                this.transform.Translate(Vector2.left * movementOffset * Time.deltaTime);
            }
            if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && !crosshairRight)
            {
                if (movements < 61 && tutorialStage == 0) { movements++; }
                this.transform.Translate(Vector2.right * movementOffset * Time.deltaTime);
            }
        }

        // advance tutorial stage for 0 and 6
        if (movements >= 60 && (tutorialStage == 0 || tutorialStage == 5))
        {
            if (!advancingTutorial)
            {
                advancingTutorial = true;
                AdvanceTutorial();
            }
            movements = 0;
        }

        // advance tutorial on transitions that need transition
        if ((Input.GetKeyUp(KeyCode.Return)) && canSkip)
        {
            if (!advancingTutorial)
            {
                advancingTutorial = true;
                AdvanceTutorial();
            }
        }

        // advance tutorial debug button
        if (Input.GetKeyUp(KeyCode.T))
        {
            if (!advancingTutorial)
            {
                advancingTutorial = true;
                AdvanceTutorial();
            }
        }

        // camera movement
        if (crosshairUp && tutorialStage >= 5 && canMove && tutorialStage != 11)
        {
            if (movements < 61 && tutorialStage == 5) { movements++; }
            cameraMovementScript.moveUp();
        }
        if (crosshairDown && tutorialStage >= 5 && canMove && tutorialStage != 11)
        {
            if (movements < 61 && tutorialStage == 5) { movements++; }
            cameraMovementScript.moveDown();
        }
        if (crosshairLeft && tutorialStage >= 5 && canMove && tutorialStage != 11)
        {
            if (movements < 61 && tutorialStage == 5) { movements++; }
            cameraMovementScript.moveLeft();
        }
        if (crosshairRight && tutorialStage >= 5 && canMove && tutorialStage != 11)
        {
            if (movements < 61 && tutorialStage == 5) { movements++; }
            cameraMovementScript.moveRight();
        }

        // logic for star being clicked
        if (Input.GetKeyUp(KeyCode.Return) && canClick && starText.text == "Alpheratz" && tutorialStage >= 4)
        {
            canClick = false;
            Invoke("CooledDown", coolDown);
            ChangeTargetStarColor();
            if (tutorialStage == 4)
            {
                if (!advancingTutorial)
                {
                    advancingTutorial = true;
                    AdvanceTutorial();
                    Debug.Log("call 3");
                }
            }
        }

        //logic for clicking a star that is not the target to play sound effect
        if (Input.GetKeyUp(KeyCode.Return) && canClick && starText.text != "Alpheratz" && starText.text != "")
        {
            canClick = false;
            Invoke("CooledDown", coolDown);
            incorrectSound.Play();
        }

        // logic for clicking go button at end of tutorial
        if (Input.GetKeyUp(KeyCode.Return) && goButtonHovered)
        {
            if (!advancingTutorial)
            {
                advancingTutorial = true;
                AdvanceTutorial();
            }
        }

        // logic for starting or skipping tutorial 
        if (Input.GetKeyUp(KeyCode.Return) && tutorialStage <= 0)
        {
            // Skip tutorial
            if (startButtonHovered)
            {
                tutorialStage = 11;
                advancingTutorial = true;
                AdvanceTutorial();
            }
            // Start tutorial
            if (!advancingTutorial && reviewButtonHovered)
            {
                advancingTutorial = true;
                AdvanceTutorial();
            }
        }

    }

    // checks if crosshair has collided with a trigger
    void OnTriggerEnter2D(Collider2D other)
    {
        // turns on movement directions
        switch (other.gameObject.name)
        {
            case "right":
                crosshairRight = true;
                break;
            case "left":
                crosshairLeft = true;
                break;
            case "up":
                crosshairUp = true;
                break;
            case "down":
                crosshairDown = true;
                break;
            // Different button hover behaviors
            case "StartButton":
                startButtonHovered = true;
                GameObject.Find("StartButton").GetComponent<Image>().color = Color.green;
                break;
            case "ReviewButton":
                reviewButtonHovered = true;
                GameObject.Find("ReviewButton").GetComponent<Image>().color = Color.green;
                break;
            case "GoButton":
                goButtonHovered = true;
                GameObject.Find("GoButton").GetComponent<Image>().color = Color.green;
                break;
            default: // otherwise it's a star
                showBox(other.gameObject.name);
                break;
        }
    }

    // checks if crosshair is no longer colliding with a trigger
    void OnTriggerExit2D(Collider2D other)
    {
        // turns off movement directions
        switch (other.gameObject.name)
        {
            case "right":
                crosshairRight = false;
                break;
            case "left":
                crosshairLeft = false;
                break;
            case "up":
                crosshairUp = false;
                break;
            case "down":
                crosshairDown = false;
                break;
            // Different button hover behaviors
            case "StartButton":
                startButtonHovered = false;
                GameObject.Find("StartButton").GetComponent<Image>().color = Color.white;
                break;
            case "ReviewButton":
                reviewButtonHovered = false;
                GameObject.Find("ReviewButton").GetComponent<Image>().color = Color.white;
                break;
            case "GoButton":
                goButtonHovered = false;
                GameObject.Find("GoButton").GetComponent<Image>().color = Color.white;
                break;
            default: // otherwise it's a star
                hideBox();
                break;
        }
    }

    // shows box by crosshair star box
    public void showBox(string star)
    {

        // shows star name box for stages 3-5
        if (star != "Main Camera" && tutorialStage >= 3 && tutorialStage <= 5)
        {
            textBox.gameObject.SetActive(true);
            blackBox.gameObject.SetActive(true);
            starText.text = star;
        }
        if (tutorialStage == 3) // if shown on stage 3, advance stage
        {
            if (!advancingTutorial)
            {
                advancingTutorial = true;
                AdvanceTutorial();
            }
        }
    }

    // hides box by crosshair
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

    void HideGameComponents()
    {
        scoreUI.gameObject.SetActive(false);
        hideBox();
        timer.gameObject.SetActive(false);
        TargetStarText.GetComponent<Text>().enabled = false;
        tutorialPanel.gameObject.SetActive(false);
        alpheratz.GetComponent<Renderer>().enabled = false;
        navi.GetComponent<Renderer>().enabled = false;
        alpheratz.transform.GetChild(0).gameObject.GetComponent<Renderer>().enabled = false;
        navi.transform.GetChild(0).gameObject.GetComponent<Renderer>().enabled = false;
        GameObject.Find("GameUI_TargetBox").GetComponent<SpriteRenderer>().enabled = false;
    }

    void ShowSkipBox()  // Skips skip box visuals and enables "enter" to skip
    {
        canSkip = true;
        GameObject.Find("continueBox").GetComponent<SpriteRenderer>().enabled = true;
        GameObject.Find("continuePanel").GetComponent<Image>().enabled = true;
        GameObject.Find("continueText").GetComponent<Text>().enabled = true;

    }

    void HideSkipBox()  // Hides Skip box visuals and disables "enter" to skip
    {
        canSkip = false;
        GameObject.Find("continueBox").GetComponent<SpriteRenderer>().enabled = false;
        GameObject.Find("continuePanel").GetComponent<Image>().enabled = false;
        GameObject.Find("continueText").GetComponent<Text>().enabled = false;
    }

    // Advances the Tutorial to the next stage and implements that stage's logic
    void AdvanceTutorial()
    {
        tutorialStage++;
        if (tutorialStage >= 12) // loads game at final stage of tutorial
        {

            SceneManager.LoadScene("Game");
        }
        else if (tutorialStage == 0) // loads tutorial if player doesn't skip
        {
            StartTutorial();
        }
        else // updates tutorial text with current stage
        {
            tutorialPanelText.text = instructions[tutorialStage];
        }

        Debug.Log("Tutorial Advanced to Stage: " + tutorialStage);


        if (tutorialStage == 1) // shows stars
        {
            ShowSkipBox();
            alpheratz.GetComponent<Renderer>().enabled = true;
            navi.GetComponent<Renderer>().enabled = true;
            alpheratz.transform.GetChild(0).gameObject.GetComponent<Renderer>().enabled = true;
            navi.transform.GetChild(0).gameObject.GetComponent<Renderer>().enabled = true;
        }
        else if (tutorialStage == 2) // shows target star
        {
            TargetStarText.GetComponent<Text>().enabled = true;
        }
        else if (tutorialStage == 3) // move to target star
        {
            HideSkipBox();
        }
        else if (tutorialStage == 4) // select star
        {
            ShowSkipBox();
        }
        else if (tutorialStage == 5) // shows score and changes target star
        {
            HideSkipBox();
            scoreUI.gameObject.SetActive(true);
            TargetStarText.GetComponent<Text>().text = "Target: Well Done!";
        }
        else if (tutorialStage == 6) // transition to hint videos
        {
            // moves crosshair to screen center
            Vector3 centerPos = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.5f));
            this.transform.position = centerPos;
            canMove = false; // disables movement
            // shows skip box and hides starname box
            hideBox();
            ShowSkipBox();
        }
        else if (tutorialStage == 7) // hides game components and shows first hint video
        {
            // hides game components
            hideBox();
            ShowSkipBox();
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            scoreUI.gameObject.SetActive(false);
            GameObject.Find("TargetText").GetComponent<Text>().enabled = false;
            GameObject.Find("continueBox").GetComponent<SpriteRenderer>().enabled = false;
            GameObject.Find("Background Video Player").GetComponent<VideoPlayer>().enabled = false;
            // loads hint video
            video.clip = (VideoClip)Resources.Load("hintVideos/hint1");
            video.Play();
        }
        else if (tutorialStage == 8) // shows second hint video
        {
            video.clip = (VideoClip)Resources.Load("hintVideos/hint2");
            video.Play();
        }
        else if (tutorialStage == 9) // shows third hint video
        {
            video.clip = (VideoClip)Resources.Load("hintVideos/hint3");
            video.Play();
        }
        else if (tutorialStage == 10) // shows game components and hides videos, timed mission
        {
            // shows game components
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            scoreUI.gameObject.SetActive(true);
            GameObject.Find("TargetText").GetComponent<Text>().enabled = true;
            GameObject.Find("continueBox").GetComponent<SpriteRenderer>().enabled = true;
            GameObject.Find("Background Video Player").GetComponent<VideoPlayer>().enabled = true;
            timer.gameObject.SetActive(true);
            video.enabled = false; // turns off hint videos
        }
        else if (tutorialStage == 11) // show transition to game
        {
            // hide all game elements
            hideBox();
            HideSkipBox();
            HideGameComponents();
            GameObject.Find("TargetText").GetComponent<Text>().enabled = false;
            GameObject.Find("continueBox").GetComponent<SpriteRenderer>().enabled = false;
            // makes the crosshair visible and moveable
            canMove = true;
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            // swap out background video
            GameObject.Find("Background Video Player").GetComponent<VideoPlayer>().clip = (VideoClip)Resources.Load("RTTM_Overlay_Go");
            // show final scene objects
            finalPanel.gameObject.SetActive(true);
            goButton.SetActive(true);
        }
        advancingTutorial = false;
    }
}
