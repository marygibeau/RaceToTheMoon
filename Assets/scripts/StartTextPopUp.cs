using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StartTextPopUp : MonoBehaviour
{

    public GameObject starText;
    // public GameObject star;
    public bool isEntered;

    // Ensures text is not on display when the game is started
    public void Start()
    {
        starText.gameObject.GetComponent<Text>().enabled = false;
        isEntered = false;
    }

    // If the start is hovered over, show the star name
    void OnTriggerEnter2D(Collider2D other)
    {
        // starText.gameObject.SetActive(true);
        starText.gameObject.GetComponent<Text>().enabled = true;
        isEntered = true;
        
    }
    // Once crosshair is no longer over star, hide the star name
    void OnTriggerExit2D(Collider2D other)
    {
        // starText.gameObject.SetActive(false);
        starText.gameObject.GetComponent<Text>().enabled = false;
        isEntered = false;
    }


}
