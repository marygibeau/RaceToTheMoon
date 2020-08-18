using System.Collections;
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
    public HUDHandler HUD;

    // Start is called before the first frame update
    void Start()
    {
        state = 0;
        playTutorial = (PlayerPrefs.GetString("Replay") == "true" ? false : true);
        if (playTutorial)
        {
            HUD.DimStarBar();
            HUD.DimScoreAndTimerBox();
            HUD.DimCalibrationGraphic();
        } else {
            timer.StartGame();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonUp("Fire1") && playTutorial)
        {
            state++;
            ChangeState();
        }
    }

    void ChangeState()
    {
        Debug.Log("changing state to " + state);
        switch (state)
        {
            case 1: //timer and score
                HUD.DimTargetStarBox();
                HUD.UndimScoreAndTimerBox();
                tutorialStar.SetActive(false);
                tutorialPanel.GetComponent<TextMeshProUGUI>().text = "You have 2 minutes to find all target stars and increase your score";
                break;
            case 2: // star bar and calibration graphic
                HUD.DimScoreAndTimerBox();
                HUD.UndimStarBar();
                HUD.UndimCalibrationGraphic();
                buttonText.text = "Press the Button to Start";
                tutorialPanel.GetComponent<TextMeshProUGUI>().text = "Find enough stars to get into the <#648FFF>blue zone</color> and make it to the moon";
                tutorialPanel.GetComponent<TextMeshProUGUI>().text += "\n\n";
                tutorialPanel.GetComponent<TextMeshProUGUI>().text += "If not, your mission will be <#FFB000>aborted</color>";
                break;
            default:
                // end tutorial
                timer.StartGame();
                break;
        }
    }


}
