using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class TimerScriptWithTutorial : MonoBehaviour
{
    TextMeshProUGUI timerText;
    float currentTime = 0f;
    public float startingTime = 5f;
    Boolean gameOver = false;
    Boolean gameStarted = false;
    public GameObject countdownPanelText;
    TextMeshProUGUI countdownPanelTextComp;
    public GameObject countdownPanel;
    float currentTimeCountdown = 10f;
    public CrosshairMovementScriptWithTutorial game;
    string countdownText = "Try and find as many stars as you can before launch to help the astronauts safely reach the moon. \n \n STAR SELECTION PROCESS BEGINNING IN\n";

    LevelManager lvlr;

    // Start is called before the first frame update
    void Start()
    {
        game.setCanMove(false);
        currentTime = startingTime;
        timerText = gameObject.GetComponent<TextMeshProUGUI>();
        timerText.text = countdownPanelText + currentTimeCountdown.ToString();
        countdownPanelTextComp = countdownPanelText.GetComponent<TextMeshProUGUI>();
        lvlr = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver && gameStarted)
        {
            currentTime -= 1 * Time.deltaTime;
        } else 
        {
            currentTimeCountdown -= 1 * Time.deltaTime;
            countdownPanelTextComp.text = countdownText + ((int)currentTimeCountdown + 1).ToString();
            if(currentTimeCountdown <= 0 && !gameOver) 
            {
                gameStarted = true;
                countdownPanel.SetActive(false);
                game.setCanMove(true);
                game.startTutorial();
            }
        }
        //changes color to red when time is below 10 seconds
        if (currentTime <= 10 && currentTime > 0)
        {
            timerText.color = Color.red;
        }

        if (Math.Ceiling(currentTime) % 60 != 0)
        {
            timerText.text = ((int)(currentTime / 60)).ToString() + ":" + (Math.Ceiling(currentTime) % 60).ToString("00");
        }
        else if (currentTime > 0)
        {
            //handling a weird edge case for when the seconds are 00 and the minute decrements a second early
            timerText.text = ((int)(currentTime / 60) + 1).ToString() + ":" + (Math.Ceiling(currentTime) % 60).ToString("00");
        }
        else
        {
            timerText.text = "0:00";
            //if the game isn't over and the timer is at 0, end the game
            if (!gameOver)
            {
                LoadEndScreen();
                gameOver = true;
            }
        }
        //ensuring the timer doesn't go into the negatives
        if (currentTime <= 0)
        {
            currentTime = 0;
        }

    }

    public void stopTimer() {
        gameOver = true;
    }

    public int GetTimeLeft() {
        return (int) Math.Ceiling(currentTime);
    }

    public void LoadEndScreen()
    {
        // get crosshair to show launch button
        CrosshairMovementScriptWithTutorial crosshair = GameObject.Find("crosshair").GetComponent<CrosshairMovementScriptWithTutorial>();
        crosshair.gameOverActivate();
        
    }
}
