using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FinalScoreTextScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("started text");
        this.GetComponent<TextMeshProUGUI>().text = generateScoreText();
    }

    public string generateScoreText()
    {
        int currentScore = PlayerPrefs.GetInt("finalScore");
        return currentScore.ToString();
    }

}

