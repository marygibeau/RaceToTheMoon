using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;


public class StarListScript : MonoBehaviour
{
    private TextMeshProUGUI starList;
  

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<TextMeshProUGUI>().text = generateStarList();
        starList = this.GetComponent<TextMeshProUGUI>();
        
    }

    public string generateStarList()
    {
        string starsFoundText = "";
        int starsFound = PlayerPrefs.GetInt("starsFound");

        for (int x = 0; x < starsFound; x++)
        {
            string getString = "Star_" + (x).ToString();
            starsFoundText += PlayerPrefs.GetString(getString);
            if (x < starsFound -1 ){ starsFoundText += "  --  "; }
            
        }

        return starsFoundText;
    }
  
}
