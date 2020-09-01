using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ReadyGoHandler : MonoBehaviour
{

    public TimerScriptWithTutorial timer;
    public bool goTime = false;
    public bool startGame = false;

    private void Update()
    {
        if (goTime)
        {
            this.gameObject.GetComponent<TextMeshProUGUI>().text = "<#FFB000>Go!</color>";
            goTime = false;
        }
        if(startGame) {
            timer.StartGame();
            this.gameObject.SetActive(false);
            startGame = false;
        }
    }
}
