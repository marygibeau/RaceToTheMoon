﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialHandler : MonoBehaviour
{
    private int state;
    private bool playTutorial;
    public GameObject tutorialStar;
    public TextMeshProUGUI buttonText;
    public TimerScriptWithTutorial timer;
    public GameObject tutorialPanel;
    public GameObject ReadyGoText;
    private bool startingGame = false;
    public HUDHandler HUD;

    // Start is called before the first frame update
    void Start()
    {
        state = 0;
        playTutorial = (PlayerPrefs.GetString("Replay") == "true" ? false : true);
        if (playTutorial)
        {
            HUD.HideStarBar();
            HUD.HideScoreAndTimerBox();
            HUD.HideCalibrationGraphic();
            ReadyGoText.SetActive(false);
            // tutorialPanel.GetComponent<Animator>().SetTrigger("State Change");
            // tutorialPanel.GetComponent<Animation>().Play("Tutorial Text Fade In");
        }
        else
        {
            Debug.Log("Starting game from tutorial handler");
            ReadyGoText.SetActive(true);
            ReadyGoText.GetComponent<Animator>().SetTrigger("PlayAnimation");
            startingGame = true;
            // timer.StartGame();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonUp("Fire1") && playTutorial && !startingGame)
        {
            state++;
            ChangeState();
        }
    }

    public void ReadyGo()
    {
        tutorialStar.SetActive(false);
        tutorialPanel.SetActive(false);
        buttonText.text = "";
        ReadyGoText.SetActive(true);
        ReadyGoText.GetComponent<Animator>().SetTrigger("PlayAnimation");
        timer.ImmobilizePlayer();
        startingGame = true;
    }

    void ChangeState()
    {
        Debug.Log("changing state to " + state);
        switch (state)
        {
            case 1: //timer and score
                HUD.DimTargetStarBox();
                HUD.ShowScoreAndTimerBox();
                tutorialStar.SetActive(false);
                tutorialPanel.GetComponent<Animator>().SetTrigger("State Change");
                // tutorialPanel.GetComponent<Animation>().Play("Tutorial Text Fade In");
                tutorialPanel.GetComponent<TextMeshProUGUI>().text = "You have 2 minutes to find all target stars and increase your score";
                break;
            case 2: // star bar and calibration graphic
                HUD.DimScoreAndTimerBox();
                HUD.ShowStarBar();
                HUD.ShowCalibrationGraphic();
                tutorialPanel.GetComponent<Animator>().SetTrigger("State Change");
                // tutorialPanel.GetComponent<Animation>().Play("Tutorial Text Fade In");
                buttonText.text = "Press the Button to Start";
                tutorialPanel.GetComponent<TextMeshProUGUI>().text = "Find enough stars to get into the <#648FFF>blue zone</color> and make it to the moon";
                tutorialPanel.GetComponent<TextMeshProUGUI>().text += "\n\n";
                tutorialPanel.GetComponent<TextMeshProUGUI>().text += "If not, your mission will be <#FFB000>aborted</color>";
                break;
            default:
                // end tutorial
                HUD.UndimHUD();
                // timer.StartGame();
                ReadyGo();
                break;
        }
    }


}
