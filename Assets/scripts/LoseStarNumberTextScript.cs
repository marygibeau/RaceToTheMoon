using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoseStarNumberTextScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<TextMeshProUGUI>().text = generateStarNoText();
    }

    // Update is called once per frame
    public string generateStarNoText()
    {
        int stars = PlayerPrefs.GetInt("starsFound");
        Debug.Log("Stars found = " + stars);
        if (stars == 1)
        {
            return "And You Found 1 Star";
        } else
        {
            return "And You Found "+ stars.ToString() +" Stars";
        }
    }
}
