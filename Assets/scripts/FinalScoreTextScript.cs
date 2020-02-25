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
            return "Score: 00000";
        }
        if (currentScore < 10000)
        {
            return "Score: " + ("0" + currentScore);
        }
        else return "Score: " + currentScore.ToString();
    }

}

