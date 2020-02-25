using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTextPopUp : MonoBehaviour
{

    public GameObject starText;

    // Ensures text is not on display when the game is started
    public void Start()
    {
        if(starText.active == true)
        {
            starText.SetActive(false);
        }
        
    }

    // If the start is hovered over, show the star name
    void OnTriggerEnter2D(Collider2D other)
    {
        starText.SetActive(true);
        
    }
    // Once reticle is no longer over star, hide the star name
    void OnTriggerExit2D(Collider2D other)
    {
        starText.SetActive(false);
    }


}
