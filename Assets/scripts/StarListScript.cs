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
    private float scrollPosition;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Text>().text = generateStarList();
        starList = this.GetComponent<Text>();
        startPosition = starList.transform.position;

        Debug.Log(startPosition);
        Debug.Log(starList.transform.localPosition);

        Debug.Log("Initial Y (position): " + starList.transform.position.y);
        Debug.Log("Initial Y (localPosition): " + starList.transform.localPosition.y);
    }

    // Update is called once per frame
    void Update(){
        float height = starList.preferredHeight - 6.0f;
        scrollPosition = 0.0f + startPosition.y;
        Debug.Log("Text Height: " + height);

        starList.transform.position = new Vector3(startPosition.x, scrollPosition % height, startPosition.z);

        scrollPosition += scrollSpeed * 20 * Time.deltaTime;

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
}
