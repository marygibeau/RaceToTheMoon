using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;


public class StarListScript : MonoBehaviour
{
    private Text starList;
    private Vector3 startPosition;
    private float scrollSpeed = 2.5f;
    private float scrollPosition = 0.0f;
    private float width;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Text>().text = generateStarList();
        starList = this.GetComponent<Text>();
        startPosition = starList.transform.position;
        
        scrollPosition = 0.0f;
        width = starList.preferredWidth;
        
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
     // Update is called once per frame
    void Update()
    {
        
        starList.transform.localPosition = new Vector3(-scrollPosition % width, startPosition.y, startPosition.z);

        Debug.Log(scrollPosition);

        scrollPosition += scrollSpeed * 20 * Time.deltaTime;


    }
}
