using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class videoViewControls : MonoBehaviour
{

    private VideoPlayer videoPlayer;

    private void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer> ();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void playPause()
    {
        if (videoPlayer.isPlaying)
        {
            videoPlayer.Pause();
        } else
        {
            videoPlayer.Play();
        }
    }
}
