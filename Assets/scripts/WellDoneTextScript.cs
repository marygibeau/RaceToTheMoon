using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WellDoneTextScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("started well done text");
        this.GetComponent<TextMeshProUGUI>().text = generateWellDoneText();
    }

    public string generateWellDoneText()
    {
        int currentScore = PlayerPrefs.GetInt("finalScore");
        if(currentScore < 3000)
        {
            return "Well Done <#FFB000>Cadet! </color>" + '\n' + "You Scored";
        } else if (currentScore >= 3000 && currentScore < 6000)
        {
            return "Well Done <#FFB000>Astronaut!</color>" + '\n' + "You Scored";
        } else if (currentScore >= 6000 && currentScore < 9000)
        {
            return "Well Done <#FFB000>Super Space Trooper!</color>" + '\n' + "You Scored";
        } else if (currentScore >= 9000 && currentScore < 12000)
        {
            return "Well Done <#FFB000>Space Captain!</color>" + '\n' + "You Scored";
        } else
        {
            return "Well Done <#FFB000>Star Commander!</color>" + '\n' + "You Scored";
        }
    }


}
