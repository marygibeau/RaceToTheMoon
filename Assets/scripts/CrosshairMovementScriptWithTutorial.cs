using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class CrosshairMovementScriptWithTutorial : MonoBehaviour
{

    // movement variables
    float movementOffset = 4.0f;
    public CameraMovement cameraMovementScript;
    public Camera mainCamera;
    public GameObject backgroundImage;
    private bool crosshairUp = false;
    private bool crosshairDown = false;
    private bool crosshairLeft = false;
    private bool crosshairRight = false;
    private bool canMove = true;

    // UI variables
    TextMeshProUGUI scoreUI;
    TextMeshProUGUI starText;
    GameObject blackBox;
    GameObject textBox;
    private int currentScore = 0;
    public int scoreIncrement = 1000;

    // star clicking variables
    bool canClick = true;
    public float coolDown = 1.0f;
    TargetStar targetScript;
    GameObject targetStar;
    float timeSinceLastStar = 0;
    GameObject lastStar;
    

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
    public GameObject launchButton;
    public GameObject launchPanel;
    bool gameOver;
    bool launchButtonHovered;
    TimerScriptWithTutorial timer;

    //tutorial variables
    public GameObject tutorial;

    // hint variables
    Boolean isAudioHint;
    Boolean isArrowHint;
    public GameObject upArrow;
    public GameObject downArrow;
    public GameObject leftArrow;
    public GameObject rightArrow;
    private GameObject blinkStar;
    private Vector3 scaleChange = new Vector3(-0.01f, -0.01f, -0.01f);
    private bool firstHintCalled = false;

    void Start()
    {
        // score set up
        scoreUI = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        scoreUI.text = "Score: 00000";
        // set up star name box variables
        blackBox = this.transform.GetChild(0).gameObject;
        starText = this.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>();
        textBox = this.transform.GetChild(2).gameObject;
        // grab level manager and targetStar scripts
        lvlr = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        targetScript = GameObject.Find("TargetStarHandler").GetComponent<TargetStar>();
        // hide arrows
        initializeArrows();
        // sound variables set up
        selectionSound = gameObject.GetComponents<AudioSource>()[1];
        hintSound = gameObject.GetComponents<AudioSource>()[0];
        incorrectSound = gameObject.GetComponents<AudioSource>()[2];
        closeSound = (AudioClip)Resources.Load("sounds/boopClose");
        midSound = (AudioClip)Resources.Load("sounds/boopMid");
        farSound = (AudioClip)Resources.Load("sounds/boopFar");
        rapidSound = (AudioClip)Resources.Load("sounds/boopRapid");
        // hide launch panel
        launchButton.gameObject.SetActive(false);
        launchPanel.gameObject.SetActive(false);
        // hide tutorial
        tutorial.gameObject.SetActive(false);
        // game state variable set up
        gameOver = false;
        launchButtonHovered = false;
        // timer setup and hide star name box
        timer = GameObject.Find("Timer").GetComponent<TimerScriptWithTutorial>();
        hideBox();
    }

    // Update is called once per frame
    void Update()
    {
        // keeps track of time since last star for hint purposes
        timeSinceLastStar += 1 * Time.deltaTime;

        // turn on different hints depending on how long since last star
        if (Math.Ceiling(timeSinceLastStar) % 60 == 5 && !gameOver)
        {
            StartFirstHint();
        }
        else if (Math.Ceiling(timeSinceLastStar) % 60 == 10 && !gameOver)
        {
            StartSecondHint();
        }
        else if (Math.Ceiling(timeSinceLastStar) % 60 == 15 && !gameOver)
        {
            StartThirdHint();
        }

        // crosshair movement
        if (canMove)
        {
            if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && !crosshairUp)
            {
                this.transform.Translate(Vector2.up * movementOffset * Time.deltaTime);
            }
            if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && !crosshairDown)
            {
                this.transform.Translate(Vector2.down * movementOffset * Time.deltaTime);
            }
            if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && !crosshairLeft)
            {
                this.transform.Translate(Vector2.left * movementOffset * Time.deltaTime);
            }
            if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && !crosshairRight)
            {
                this.transform.Translate(Vector2.right * movementOffset * Time.deltaTime);
            }
        }

        // camera movement
        if (crosshairUp && !gameOver)
        {
            cameraMovementScript.moveUp();
        }
        if (crosshairDown && !gameOver)
        {
            cameraMovementScript.moveDown();
        }
        if (crosshairLeft && !gameOver)
        {
            cameraMovementScript.moveLeft();
        }
        if (crosshairRight && !gameOver)
        {
            cameraMovementScript.moveRight();
        }

        // logic for star being clicked
        if (Input.GetKeyUp(KeyCode.Return) && canClick && starText.text == targetScript.GetTarget() && !gameOver)
        {
            canClick = false;
            increaseScore(scoreIncrement);
            Invoke("CooledDown", coolDown);
            ResetHints();
            UpdateTargetStarDebug();
            // turn off tutorial if found first star
            if (getStars() >= 1) {
                endTutorial();
            }
        }

        //logic for clicking a star that is not the target to play sound effect
        if (Input.GetKeyUp(KeyCode.Return) && canClick && starText.text != targetScript.GetTarget()
        && starText.text != "" && !gameOver)
        {
            lastStar = GameObject.Find(starText.text);
            lastStar.transform.GetChild(2).GetComponent<SpriteRenderer>().enabled = true;
            Invoke("disableX", .6F);
            canClick = false;
            Invoke("CooledDown", coolDown);
            incorrectSound.Play();
        }

        // save score and load next screen when launch button pressed at end of game
        if (Input.GetKeyUp(KeyCode.Return) && launchButtonHovered)
        {
            lvlr.LoadNextLevelWithStarListAndTimeLeft(getStarsCollectedList(), timer.GetTimeLeft());
        }

        //testing increaseScore
        if (Input.GetKeyDown(KeyCode.Q) && !gameOver)
        {
            increaseScore(scoreIncrement);
        }

        // testing load with final score
        if (Input.GetKeyDown(KeyCode.E) && !gameOver)
        {
            lvlr.LoadNextLevelWithStarListAndTimeLeft(getStarsCollectedList(), GameObject.Find("Timer").GetComponent<TimerScript>().GetTimeLeft());
        }

        // cycle target star
        if (Input.GetKeyDown(KeyCode.T) || Input.GetKeyDown(KeyCode.N) && !gameOver)
        {
            UpdateTargetStarDebug();
        }

        // audio hint
        if (isAudioHint && !gameOver)
        {
            // gets distance from crosshair to target star
            double distanceToTarget = (Vector2.Distance(gameObject.transform.position, GameObject.Find(targetScript.GetTarget()).transform.position));
            // need to keep track to see if the sound actually changed so we can call Play on the Audio Source
            AudioClip prevSound = hintSound.clip;
            if (distanceToTarget > 15)
                hintSound.clip = farSound;
            else if (distanceToTarget > 7) hintSound.clip = midSound;
            else if (distanceToTarget > 1.5) hintSound.clip = closeSound;
            else hintSound.clip = rapidSound;
            if (prevSound != hintSound.clip) hintSound.Play();

        }

        // updates arrow hint while arrow hint is active
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
            case "launch button": // launch button hover behavior
                launchButtonHovered = true;
                GameObject.Find("launch button").GetComponent<Image>().color = Color.green;
                break;
            default:
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
            case "launch button": // launch button hover behavior
                launchButtonHovered = false;
                GameObject.Find("launch button").GetComponent<Image>().color = Color.white;
                break;
            default:
                hideBox();
                break;
        }
    }

    // shows star name box
    public void showBox(string star)
    {
        if (star != "Main Camera" && star != "Button" && !gameOver)
        {
            textBox.gameObject.SetActive(true);
            blackBox.gameObject.SetActive(true);
            starText.text = star;
        }
    }

    // hides star name box
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

    // updates score ui text
    public void updateScoreText(int score)
    {
        if (currentScore < 10000)
        {
            scoreUI.text = "Score: " + ("0" + currentScore);
        }
        else scoreUI.text = "Score: " + currentScore.ToString();
    }

    // cooldown function for star clicking
    void CooledDown()
    {
        canClick = true;
    }

    // turns off x for wrong star behavior
    void disableX()
    {
        lastStar.transform.GetChild(2).GetComponent<SpriteRenderer>().enabled = false;
    }

    public int getScore()
    {
        return currentScore;
    }

    // getter for target script to get number of stars found
    public int getStars()
    {
        return targetScript.GetNumberOfStarsFound();
    }

    // getter for target script to get a list of stars found
    public List<string> getStarsCollectedList()
    {
        return targetScript.GetNamesOfStarsFound();
    }

    // changes color of a star once it has been found
    void ChangeTargetStarColor()
    {
        GameObject targetStar = GameObject.Find(targetScript.GetTarget());
        GameObject targetStarSquare = targetStar.transform.GetChild(0).gameObject;
        GameObject targetStarCircle = targetStar.transform.GetChild(1).gameObject;
        targetStarSquare.GetComponent<SpriteRenderer>().enabled = false;
        targetStarCircle.GetComponent<SpriteRenderer>().enabled = true;
    }

    // updates target star and logs information in console
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

    // updates target star
    void UpdateTargetStar()
    {
        ChangeTargetStarColor();
        // TODO
        if (!gameOver)
        {
            targetScript.UpdateTarget();
            if (targetScript.GetTarget() == "done")
            {
                gameOverActivate();
            }
            else
            {
                targetStar = GameObject.Find(targetScript.GetTarget());
            }
        }
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

    // updates which arrows are active depending on screen position relative to target star
    void UpdateArrows()
    {
        // turns off arrows if target star is visible
        if (targetStar.GetComponent<Renderer>().isVisible)
        {
            rightArrow.gameObject.SetActive(false);
            leftArrow.gameObject.SetActive(false);
            upArrow.gameObject.SetActive(false);
            downArrow.gameObject.SetActive(false);
        }
        else
        {
            // gets screen points
            Vector3 topRight = mainCamera.ViewportToWorldPoint(new Vector3(1.0f, 1.0f, 1.0f));
            Vector3 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0.0f, 0.0f, 1.0f));

            // get target star location
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

    // saves arrows and makes them invisible
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

    // turns off all hints and resents time since last star
    void ResetHints()
    {
        StopFirstHint();
        StopSecondHint();
        StopThirdHint();
        timeSinceLastStar = 0;
    }

    // shows end of game launch panel
    public void showLaunchInfo()
    {
        launchButton.gameObject.SetActive(true);
        launchPanel.gameObject.SetActive(true);
        GameObject.Find("terminalReticleSimpleGreen").gameObject.SetActive(false);
        ResetHints();
    }

    public void startTutorial() {
        tutorial.gameObject.SetActive(true);
    }


    public void endTutorial() {
        tutorial.gameObject.SetActive(false);
    }

    // begins end of game sequence
    public void gameOverActivate()
    {
        gameOver = true;
        timer.stopTimer();
        hideBox();
        showLaunchInfo();
    }

    // setter for can move
    public void setCanMove(Boolean b)
    {
        canMove = b;
    }

}
