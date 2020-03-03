using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalScoreTextScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("started text");
        this.GetComponent<Text>().text = generateScoreText();
    }

    public string generateScoreText()
    {
        int currentScore = PlayerPrefs.GetInt("finalScore");
        if(currentScore == 0) {
            return "You Scored 00000 Points";
        }
        if (currentScore < 10000)
        {
            return "You Scored " + ("0" + currentScore) + " Points";
        }
        else return "You Scored " + currentScore.ToString() + " Points";
    }

}

