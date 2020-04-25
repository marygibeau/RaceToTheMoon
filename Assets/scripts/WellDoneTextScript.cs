using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WellDoneTextScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("started well done text");
        this.GetComponent<Text>().text = generateWellDoneText();
    }

    public string generateWellDoneText()
    {
        int currentScore = PlayerPrefs.GetInt("finalScore");
        if(currentScore < 3000)
        {
            return "Well Done Astronaut!" + '\n' + "You Scored";
        } else if (currentScore >= 3000 && currentScore < 6000)
        {
            return "Well Done Master Chief!" + '\n' + "You Scored";
        } else if (currentScore >= 6000 && currentScore < 9000)
        {
            return "Well Done Super Space Trooper!" + '\n' + "You Scored";
        } else if (currentScore >= 9000 && currentScore < 12000)
        {
            return "Well Done Ultimate Jedi Master!" + '\n' + "You Scored";
        } else
        {
            return "Well Done Star Commander!" + '\n' + "You Scored";
        }
    }


}
