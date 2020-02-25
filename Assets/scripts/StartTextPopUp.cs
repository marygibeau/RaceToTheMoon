using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StartTextPopUp : MonoBehaviour
{

    public GameObject starText;
    public GameObject star;
    public bool isEntered;

    // Ensures text is not on display when the game is started
    public void Start()
    {
        starText.gameObject.SetActive(false);
        print("All should be clear");
    }

    public void Update()
    {
    CircleCollider2D coll = star.GetComponent<CircleCollider2D>();
    Vector4 reticlePosition = GameObject.Find("reticle").transform.position;
        if (!coll.OverlapPoint(reticlePosition))
        {
            starText.gameObject.SetActive(false);
        }
        else
        {
            starText.gameObject.SetActive(true);
        }
    }

    // If the start is hovered over, show the star name
    void OnTriggerEnter2D(Collider2D other)
    {
        starText.gameObject.SetActive(true);
        isEntered = true;
        
    }
    // Once reticle is no longer over star, hide the star name
    void OnTriggerExit2D(Collider2D other)
    {
        starText.gameObject.SetActive(false);
        isEntered = false;
    }


}
