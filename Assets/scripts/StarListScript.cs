using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;


public class StarListScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Text>().text = generateStarList();
    }

    // Update is called once per frame
    public string generateStarList()
    {
        string starsFoundText = "";
        int starsFound = PlayerPrefs.GetInt("starsFound");
        for (int x = 0; x < starsFound; x++)
        {
            string getString = "Star_" + (x).ToString();
            starsFoundText += PlayerPrefs.GetString(getString);
            starsFoundText += '\n';


            Debug.Log(getString);

          
        }

        return starsFoundText;
    }
}
