using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class TimerScriptWithTutorial : MonoBehaviour
{
    TextMeshProUGUI timerText;
    float gameTime = 120f;
    Boolean gameOver = false;
    Boolean gameStarted = false;
    public GameObject countdownPanelText;
    TextMeshProUGUI countdownPanelTextComp;
    public GameObject countdownPanel;
    public CrosshairMovementScriptWithTutorial game;

    LevelManager lvlr;

    // Start is called before the first frame update
    void Start()
    {
        game.setCanMove(false);
        timerText = gameObject.GetComponent<TextMeshProUGUI>();
        lvlr = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Return)) {
            StartGame();
        }

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

        generateTimerText();
    }

    void StartGame()
    {
        gameStarted = true;
        countdownPanel.SetActive(false);
        game.setCanMove(true);
        game.startTutorial();
    }

    void generateTimerText()
    {
        int timeLeft = GetTimeLeft();
        //changes color to red when time is below 10 seconds
        if (gameTime <= 10 && gameTime > 0)
        {
            timerText.color = Color.red;
        }

        timerText.text = ((int)(timeLeft / 60)).ToString() + ":" + (timeLeft % 60).ToString("00");

        if (gameTime <= 0)
        {
            timerText.text = "0:00";
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

    }
}
