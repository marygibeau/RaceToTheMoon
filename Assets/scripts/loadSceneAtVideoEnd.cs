using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class LoadSceneAtVideoEnd : MonoBehaviour
{
    public VideoPlayer VideoPlayer; // Drag & Drop the GameObject holding the VideoPlayer component
    public LevelManager lvlr;

    void Start()
    {
        VideoPlayer.loopPointReached += LoadScene;
        lvlr = Object.FindObjectOfType<LevelManager>();
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
