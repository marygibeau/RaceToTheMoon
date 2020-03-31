using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCinematic : MonoBehaviour
{

    // Variables for videoPlayer
    public GameObject videoPlayer;
    public int timeToStop;


    // Start is called before the first frame update
    void Start()
    {
        videoPlayer.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (true)
        {
            videoPlayer.SetActive(true);
            Destroy(videoPlayer, timeToStop);
        }
        
    }
}
