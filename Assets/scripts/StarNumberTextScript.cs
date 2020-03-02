using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarNumberTextScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Text>().text = generateStarNoText();
    }

    // Update is called once per frame
    public string generateStarNoText()
    {
        int stars = PlayerPrefs.GetInt("starsFound");
        if(stars == 0)
        {
            return "And Found 0 Stars";
        } else if (stars == 1)
        {
            return "And Found 1 Star";
        } else
        {
            return "And Found "+ stars.ToString() +" Stars";
        }
    }
}
