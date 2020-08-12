using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class TimerScriptWithTutorial : MonoBehaviour
{
    TextMeshProUGUI timerText;
    float originalTimeAmount = 120f;
    float gameTime;
    Boolean gameStarted = false;
    Boolean gameOver = false;
    float timeSinceGameOver;
    public GameObject countdownPanel;
    public GameObject pressButtonObject;
    public CrosshairMovementScriptWithTutorial game;
    public Animator tutorialAnimator;
    private Image pieChartFull;
    private Image pieChartEmpty;

    LevelManager lvlr;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetString("Replay") == "false")
        {
            game.setCanMove(false);
        }
        timerText = gameObject.GetComponent<TextMeshProUGUI>();
        lvlr = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        pieChartEmpty = this.transform.GetChild(0).gameObject.GetComponent<Image>();
        pieChartFull = this.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Image>();
        gameTime = originalTimeAmount;
        timeSinceGameOver = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {

        if (!gameOver && gameStarted)
        {
            gameTime -= 1 * Time.deltaTime;
        }

        if (gameTime <= 0)
        {
            //if the game isn't over and the timer is at 0, end the game
            if (!gameOver)
            {
                GameOverSequence();
                gameOver = true;
            }
        }

        if (gameOver)
        {
            timeSinceGameOver += 1 * Time.deltaTime;
            Debug.Log("time since game over = " + timeSinceGameOver);
        }

        if (timeSinceGameOver >= 30)
        {
            lvlr.LoadLevel("MainMenu");
        }

        pieChartFull.fillAmount = gameTime / originalTimeAmount;

        generateTimerText();
    }

    public void StartGame()
    {
        gameStarted = true;
        game.setCanMove(true);
        game.setCanClick(true);
        game.endTutorial();
        tutorialAnimator.SetBool("Ready To Start", false);
        
    }

    void generateTimerText()
    {
        int timeLeft = GetTimeLeft();
        //changes color to red when time is below 10 seconds
        if (gameTime <= 10 && gameTime > 0)
        {
            timerText.color = new Color(0.9960784f, 0.3803922f, 0);
            pieChartFull.color = new Color(0.9960784f, 0.3803922f, 0);
            pieChartEmpty.color = new Color(0.9960784f, 0.3803922f, 0);
        }

        timerText.text = ((int)(timeLeft / 60)).ToString() + ":" + (timeLeft % 60).ToString("00");

        if (gameTime <= 0)
        {
            timerText.text = "0:00";
            timerText.color = Color.clear;
        }
    }

    public void stopTimer()
    {
        gameOver = true;
    }

    public int GetTimeLeft()
    {
        return (int)Math.Ceiling(gameTime);
    }

    void GameOverSequence()
    {
        // get crosshair to show launch button
        CrosshairMovementScriptWithTutorial crosshair = GameObject.Find("crosshair").GetComponent<CrosshairMovementScriptWithTutorial>();
        crosshair.gameOverActivate();
        gameOver = true;
    }
}
