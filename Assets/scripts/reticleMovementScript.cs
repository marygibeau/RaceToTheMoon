using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class reticleMovementScript : MonoBehaviour
{
    // Start is called before the first frame update

    // for target star logic, maintain list of stars that have been selected and stars that can be selected 
    // make sure new targetStar is in not found array before assigning

    // movement variables
    public float movementOffset = 0.1f;
    public cameraMovement cameraMovementScript;
    public Camera mainCamera;
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
    GameObject targetStar;
    public float timeSinceLastStar = 0;

    // audio variables
    AudioSource selectionSound;
    AudioSource hintSound;
    AudioClip farSound;
    AudioClip midSound;
    AudioClip closeSound;
    AudioClip rapidSound;


    // level management variables
    LevelManager lvlr;

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
        scoreUI = GameObject.Find("ScoreText").GetComponent<Text>();
        scoreUI.text = "Score: 00000";
        blackBox = this.transform.GetChild(0).gameObject;
        starText = this.transform.GetChild(1).gameObject.GetComponent<Text>();
        textBox = this.transform.GetChild(2).gameObject;
        lvlr = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        targetScript = GameObject.Find("TargetStarHandler").GetComponent<TargetStar>();
        initializeArrows();
        selectionSound = gameObject.GetComponents<AudioSource>()[1];
        hintSound = gameObject.GetComponents<AudioSource>()[0];
        closeSound = (AudioClip)Resources.Load("sounds/boopClose");
        midSound = (AudioClip)Resources.Load("sounds/boopMid");
        farSound = (AudioClip)Resources.Load("sounds/boopFar");
        rapidSound = (AudioClip)Resources.Load("sounds/boopRapid");
        hideBox();
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastStar += 1 * Time.deltaTime;

        if (Math.Ceiling(timeSinceLastStar) % 60 == 5)
        {
            StartFirstHint();
        }
        else if (Math.Ceiling(timeSinceLastStar) % 60 == 10)
        {
            StartSecondHint();
        }
        else if (Math.Ceiling(timeSinceLastStar) % 60 == 15)
        {
            StartThirdHint();
        }

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
            cameraMovementScript.moveUp();
        }
        if (reticleDown)
        {
            cameraMovementScript.moveDown();
        }
        if (reticleLeft)
        {
            cameraMovementScript.moveLeft();
        }
        if (reticleRight)
        {
            cameraMovementScript.moveRight();
        }

        // logic for star being clicked
        if (Input.GetKeyUp(KeyCode.Return) && canClick && starText.text == targetScript.GetTarget())
        {
            canClick = false;
            increaseScore(scoreIncrement);
            Invoke("CooledDown", coolDown);
            ResetHints();
            // UpdateTargetStarDebug();
            UpdateTargetStar();
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
        if (Input.GetKeyDown(KeyCode.T) || Input.GetKeyDown(KeyCode.N))
        {
            UpdateTargetStarDebug();
        }

        // audio hint
        if (isAudioHint)
        {
            // gets distance from reticle to target star
            double distanceToTarget = (Vector2.Distance(gameObject.transform.position, GameObject.Find(targetScript.GetTarget()).transform.position));
            // need to keep track to see if the sound actually changed so we can call Play on the Audio Source
            AudioClip prevSound = hintSound.clip;
            if (distanceToTarget > 15)
                hintSound.clip = farSound;
            else if (distanceToTarget > 7) hintSound.clip = midSound;
            else if (distanceToTarget > 2) hintSound.clip = closeSound;
            else hintSound.clip = rapidSound;
            if (prevSound != hintSound.clip) hintSound.Play();
            //Debug.Log(distanceToTarget);

        }

        // arrow hint
        if(isArrowHint) {
            UpdateArrows();
        }

        // first hint (star blinking)
        if(firstHintCalled)
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
        targetStar = GameObject.Find(targetScript.GetTarget());
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
        else if (distanceToTarget > 2) hintSound.clip = closeSound;
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
            if(targetX > bottomLeft.x && targetX < topRight.x) {
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
            if(targetY > bottomLeft.y && targetY < topRight.y) {
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

}
