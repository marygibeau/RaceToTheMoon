using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

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
    private int currentScore = 0;
    public int scoreIncrement = 1000;

    // star clicking variables
    bool canClick = true;
    public float coolDown = 1.0f;
    TargetStar targetScript;
    GameObject targetStar;
    public float timeSinceLastStar = 0;

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

    // hint variables
    bool isAudioHint;
    bool isArrowHint;
    public GameObject upArrow;
    public GameObject downArrow;
    public GameObject leftArrow;
    public GameObject rightArrow;
    private GameObject blinkStar;
    private Vector3 scaleChange = new Vector3(-0.01f, -0.01f, -0.01f);
    private bool firstHintCalled = false;

    // tutorial variables
    int tutorialStage;
    GameObject TargetStarText;
    int movements = 0;
    public VideoPlayer video;
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
        targetScript = GameObject.Find("TargetStarHandler").GetComponent<TargetStar>();
        initializeArrows();
        selectionSound = gameObject.GetComponents<AudioSource>()[1];
        hintSound = gameObject.GetComponents<AudioSource>()[0];
        incorrectSound = gameObject.GetComponents<AudioSource>()[2];
        closeSound = (AudioClip)Resources.Load("sounds/boopClose");
        midSound = (AudioClip)Resources.Load("sounds/boopMid");
        farSound = (AudioClip)Resources.Load("sounds/boopFar");
        rapidSound = (AudioClip)Resources.Load("sounds/boopRapid");
        video = GameObject.Find("VideoPlayer").GetComponent<VideoPlayer>();
        tutorialStage = 0;
        // instructions = "";
        // Debug.Log("instructions: " + instructions[tutorialStage]);
        tutorialPanelText = GameObject.Find("TutorialText").GetComponent<Text>();
        tutorialPanelText.text = instructions[tutorialStage];
        gameOver = false;
        timer = GameObject.Find("Timer").GetComponent<TimerScript>();
        timer.gameObject.SetActive(false);
        TargetStarText = GameObject.Find("TargetText");
        hideBox();

        TargetStarText.GetComponent<Text>().enabled = false;

        blackBox.GetComponent<Image>().enabled = false;
        starText.GetComponent<Text>().enabled = false;
        textBox.GetComponent<Renderer>().enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        // reticle movement
        if(canMove) {
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
        if (movements < 61 && movements > 0) { Debug.Log(movements); }

        if (movements >= 60 && tutorialStage == 0)
        {
            AdvanceTutorial();
            movements = 0;
        }

        if (Input.GetKeyUp(KeyCode.T))
        {
            AdvanceTutorial();
        }

        if (tutorialStage == 0){
            GameObject alpheratz = GameObject.Find("Alpheratz");
            GameObject navi = GameObject.Find("Navi");
            GameObject circle = GameObject.Find("white circle trnasparent background");

            // alpheratz.gameObject.SetActive(false);
            // dipdha.gameObject.SetActive(false);

            alpheratz.GetComponent<Renderer>().enabled = false;
            navi.GetComponent<Renderer>().enabled = false;
            circle.GetComponent<Renderer>().enabled = false;
        }

        if (tutorialStage == 1){

            GameObject alpheratz = GameObject.Find("Alpheratz");
            GameObject navi = GameObject.Find("Navi");
            GameObject circle = GameObject.Find("white circle trnasparent background");

            alpheratz.GetComponent<Renderer>().enabled = true;
            navi.GetComponent<Renderer>().enabled = true;
            circle.GetComponent<Renderer>().enabled = true;
            
            Debug.Log(movements);
        }

        if (tutorialStage == 2){
            blackBox.GetComponent<Image>().enabled = true;
            starText.GetComponent<Text>().enabled = true;
            textBox.GetComponent<Renderer>().enabled = true;
        }

        if (tutorialStage == 3){
            
            TargetStarText.GetComponent<Text>().enabled = true;
           
        }

        // camera movement
        if (reticleUp && tutorialStage >= 5)
        {
            if (movements < 65 && tutorialStage == 5) { movements++; }
            cameraMovementScript.moveUp();
        }
        if (reticleDown && tutorialStage >= 5)
        {
            if (movements < 65 && tutorialStage == 5) { movements++; }
            cameraMovementScript.moveDown();
        }
        if (reticleLeft && tutorialStage >= 5)
        {
            if (movements < 65 && tutorialStage == 5) { movements++; }
            cameraMovementScript.moveLeft();
        }
        if (reticleRight && tutorialStage >= 5)
        {
            if (movements < 65 && tutorialStage == 5) { movements++; }
            cameraMovementScript.moveRight();
        }

        // logic for star being clicked
        if (Input.GetKeyUp(KeyCode.Return) && canClick && starText.text == targetScript.GetTarget() && !gameOver && tutorialStage >= 4)
        {
            canClick = false;
            // increaseScore(scoreIncrement);
            Invoke("CooledDown", coolDown);
            ResetHints();
            ChangeTargetStarColor();
            TargetStarText.GetComponent<Text>().text = "Target: Well Done!";
            if (tutorialStage == 4) { AdvanceTutorial(); }
        }
        //logic for clicking a star that is not the target to play sound effect
        if (Input.GetKeyUp(KeyCode.Return) && canClick && starText.text != targetScript.GetTarget() && starText.text != "" && !gameOver)
        {
            canClick = false;
            Invoke("CooledDown", coolDown);
            incorrectSound.Play();
        }

        // audio hint
        if (isAudioHint && !gameOver)
        {
            // gets distance from reticle to target star
            double distanceToTarget = (Vector2.Distance(gameObject.transform.position, GameObject.Find(targetScript.GetTarget()).transform.position));
            // need to keep track to see if the sound actually changed so we can call Play on the Audio Source
            AudioClip prevSound = hintSound.clip;
            if (distanceToTarget > 15)
                hintSound.clip = farSound;
            else if (distanceToTarget > 7) hintSound.clip = midSound;
            else if (distanceToTarget > 1.5) hintSound.clip = closeSound;
            else hintSound.clip = rapidSound;
            if (prevSound != hintSound.clip) hintSound.Play();
            //Debug.Log(distanceToTarget);

        }

        // arrow hint
        if (isArrowHint && !gameOver)
        {
            UpdateArrows();
        }

        // first hint (star blinking)
        if (firstHintCalled && !gameOver)
        {
            blinkStar.transform.localScale += scaleChange;

            if (blinkStar.transform.localScale.x < 1.8f || blinkStar.transform.localScale.x > 2.4f)
            {
                scaleChange = -scaleChange;
            }
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
            case "Alpheratz":
                if(tutorialStage == 2){
                    AdvanceTutorial();
                }
                hideBox();
                break;
            default:
                hideBox();
                break;
        }
    }

    public void showBox(string star)
    {
        // Debug.Log(star);
        if (star != "Main Camera" && star != "Button" && !gameOver)
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

    void CooledDown()
    {
        canClick = true;
    }

    void ChangeTargetStarColor()
    {
        GameObject targetStarRing = GameObject.Find(targetScript.GetTarget()).transform.GetChild(0).gameObject;
        Debug.Log("targetStarRing name: " + targetStarRing.name);
        targetStarRing.GetComponent<SpriteRenderer>().color = new Color(0.1727581f, 0.945098f, 0.1215686f, 1);
    }

    void StartFirstHint()
    {
        Debug.Log("first hint started");

        firstHintCalled = true;
        blinkStar = GameObject.Find(targetScript.GetTarget());
    }

    void StopFirstHint()
    {
        Debug.Log("first hint ended");

        firstHintCalled = false;
    }

    void StartSecondHint()
    {
        Debug.Log("second hint started");

        double distanceToTarget = (Vector2.Distance(gameObject.transform.position, GameObject.Find(targetScript.GetTarget()).transform.position));
        if (distanceToTarget > 15) hintSound.clip = farSound;
        else if (distanceToTarget > 7) hintSound.clip = midSound;
        else if (distanceToTarget > 1.5) hintSound.clip = closeSound;
        else hintSound.clip = rapidSound;
        if (!isAudioHint) hintSound.Play();
        isAudioHint = true;
    }

    void StopSecondHint()
    {
        Debug.Log("second hint ended");
        isAudioHint = false;
        hintSound.Stop();
    }

    void StartThirdHint()
    {
        Debug.Log("first hint started");
        targetStar = GameObject.Find(targetScript.GetTarget());
        isArrowHint = true;
    }

    void UpdateArrows()
    {
        if (targetStar.GetComponent<Renderer>().isVisible)
        {
            rightArrow.gameObject.SetActive(false);
            leftArrow.gameObject.SetActive(false);
            upArrow.gameObject.SetActive(false);
            downArrow.gameObject.SetActive(false);
        }
        else
        {
            Vector3 topRight = mainCamera.ViewportToWorldPoint(new Vector3(1.0f, 1.0f, 1.0f));
            Vector3 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0.0f, 0.0f, 1.0f));

            float targetX = targetStar.transform.position.x;
            float targetY = targetStar.transform.position.y;
            // turn on right arrow
            if (targetX > topRight.x)
            {
                rightArrow.gameObject.SetActive(true);
                leftArrow.gameObject.SetActive(false);
            }
            // turn on left arrow
            if (targetX < bottomLeft.x)
            {
                rightArrow.gameObject.SetActive(false);
                leftArrow.gameObject.SetActive(true);
            }
            // turn off horizontal arrows
            if (targetX > bottomLeft.x && targetX < topRight.x)
            {
                rightArrow.gameObject.SetActive(false);
                leftArrow.gameObject.SetActive(false);
            }
            // turn on up arrow
            if (targetY > topRight.y)
            {
                upArrow.gameObject.SetActive(true);
                downArrow.gameObject.SetActive(false);
            }
            // turn on down arrow
            if (targetY < bottomLeft.y)
            {
                upArrow.gameObject.SetActive(false);
                downArrow.gameObject.SetActive(true);
            }
            // turn of verticle arrows
            if (targetY > bottomLeft.y && targetY < topRight.y)
            {
                upArrow.gameObject.SetActive(false);
                downArrow.gameObject.SetActive(false);
            }
        }
    }

    void StopThirdHint()
    {
        isArrowHint = false;
    }

    void initializeArrows()
    {
        upArrow = GameObject.Find("up arrow");
        downArrow = GameObject.Find("down arrow");
        leftArrow = GameObject.Find("left arrow");
        rightArrow = GameObject.Find("right arrow");
        upArrow.gameObject.SetActive(false);
        downArrow.gameObject.SetActive(false);
        leftArrow.gameObject.SetActive(false);
        rightArrow.gameObject.SetActive(false);
    }

    void ResetHints()
    {
        StopFirstHint();
        StopSecondHint();
        StopThirdHint();
        timeSinceLastStar = 0;
    }

    void AdvanceTutorial()
    {
        tutorialStage++;
        Debug.Log("tutorialpanel: " + tutorialPanelText);
        Debug.Log("Tutorial Advanced to Stage: " + tutorialStage);
        tutorialPanelText.text = instructions[tutorialStage];
        
        if(tutorialStage == 6) 
        {
            canMove = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            GameObject.Find("ScoreText").GetComponent<Text>().enabled = false;
            GameObject.Find("TargetText").GetComponent<Text>().enabled = false;
            video.clip = (VideoClip)Resources.Load("hintVideos/hint1");
            video.Play();
        } else if(tutorialStage == 7)
        {
            video.clip = (VideoClip)Resources.Load("hintVideos/hint2");
            video.Play();
        }
         else if(tutorialStage == 8) 
         {
            video.clip = (VideoClip)Resources.Load("hintVideos/hint3");
            video.Play();
         }
         else if(tutorialStage == 9) 
         {
            canMove = true;
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            GameObject.Find("ScoreText").GetComponent<Text>().enabled = false;
            GameObject.Find("TargetText").GetComponent<Text>().enabled = false;
            video.enabled = false;
         }
    }
}
