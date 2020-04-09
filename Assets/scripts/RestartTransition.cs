using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartTransition : MonoBehaviour
{

    float currentTime = 0f;
    public float startingTime = 10f;
    LevelManager lvlr;
    // Start is called before the first frame update
    void Start()
    {
        currentTime = startingTime;
        lvlr = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        Debug.Log("current time: " + currentTime);
        if (currentTime <= 0)
        {
            Debug.Log("Loading Main Menu");
            lvlr.LoadLevel("MainMenu");
        }
    }
}
