using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class LoadEndScreenAtGameEnd : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Drag & Drop the GameObject holding the VideoPlayer component
    public LevelManager lvlr;

    void Start()
    {
        videoPlayer.loopPointReached += LoadScene;
    }
    private void Update()
    {
        if (Input.GetButtonUp("Fire1"))
        {
            lvlr.LoadNextLevel();
        }
    }
    void LoadScene(VideoPlayer vp)
    {
        lvlr.LoadNextLevel();
    }
}
