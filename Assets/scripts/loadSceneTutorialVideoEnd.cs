using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class LoadSceneTutorialVideoEnd : MonoBehaviour
{
    public VideoPlayer VideoPlayer; // Drag & Drop the GameObject holding the VideoPlayer component

    void Start()
    {
        VideoPlayer.loopPointReached += LoadScene;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            SceneManager.LoadScene("Game");
        }
    }
    void LoadScene(VideoPlayer vp)
    {
        SceneManager.LoadScene("Game");
    }
}
