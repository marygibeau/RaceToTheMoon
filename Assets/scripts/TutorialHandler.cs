using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialHandler : MonoBehaviour
{
    public GameObject tutorialStar;
    public TextMeshProUGUI buttonText;
    public TimerScriptWithTutorial timer;
    public GameObject tutorialPanel;
    public GameObject ReadyGoText;
    private bool startingGame = false;

    // Start is called before the first frame update
    void Start()
    {
        ReadyGo();
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


}
