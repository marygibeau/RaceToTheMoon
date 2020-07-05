using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StarNumberTextScript : MonoBehaviour
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
            return "And Found 1 Star!";
        } else
        {
            return "And Found "+ stars.ToString() +" Stars!";
        }
    }
}
