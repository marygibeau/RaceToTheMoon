using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;


public class StarListScript : MonoBehaviour
{
    private Text starList;
    private Vector3 startPosition;
    private float scrollSpeed = 0.05f;
    private float scrollPosition = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Text>().text = generateStarList();
        starList = this.GetComponent<Text>();
        startPosition = starList.transform.position;

        if(PlayerPrefs.GetInt("starsFound") < 7){
            scrollSpeed = 0.0f;
        } else {
            scrollSpeed = 0.05f;
        }
        
        scrollPosition = 0.0f;
        float height = starList.preferredHeight;
        
        Debug.Log(height);

        Debug.Log(startPosition);
        Debug.Log(starList.transform.localPosition);

        Debug.Log("Initial Y (position): " + starList.transform.position.y);
        Debug.Log("Initial Y (localPosition): " + starList.transform.localPosition.y);
    }

    public string generateStarList()
    {
        string starsFoundText = "";
        int starsFound = PlayerPrefs.GetInt("starsFound");

        for (int x = 0; x < starsFound; x++)
        {
            string getString = "Star_" + (x).ToString();
            starsFoundText += PlayerPrefs.GetString(getString);
            starsFoundText += '\n';
        }

        return starsFoundText;
    }
     // Update is called once per frame
    void Update()
    {
        if(PlayerPrefs.GetInt("starsFound") < 7){
             starList.transform.position = new Vector3(startPosition.x, startPosition.y + 2.98f, startPosition.z);
        } else {
            float boundary = 8.0f;
            starList.transform.position = new Vector3(startPosition.x, (scrollPosition % boundary) - 1.0f, startPosition.z);

            Debug.Log(scrollPosition);

            scrollPosition += scrollSpeed * 20 * Time.deltaTime;
        }

    }
}
