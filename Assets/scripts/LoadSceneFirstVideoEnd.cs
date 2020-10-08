using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class LoadSceneFirstVideoEnd : MonoBehaviour
{
    public VideoPlayer VideoPlayer; // Drag & Drop the GameObject holding the VideoPlayer component
    public LevelManager lvlr;

    void Start()
    {
        VideoPlayer.loopPointReached += LoadScene;
    }

    private void Update()
    {
        // if (Input.GetButtonUp("Fire1"))

        // {
        //     lvlr.LoadGameWithTutorial();
        // }
    }
    void LoadScene(VideoPlayer vp)
    {
        lvlr.LoadGameWithTutorial();
    }
}
