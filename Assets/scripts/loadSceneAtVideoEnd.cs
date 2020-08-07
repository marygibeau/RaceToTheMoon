using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class LoadSceneAtVideoEnd : MonoBehaviour
{
    public VideoPlayer VideoPlayer; // Drag & Drop the GameObject holding the VideoPlayer component

    void Start()
    {
        VideoPlayer.loopPointReached += LoadScene;
    }

    private void Update()
    {
        if (Input.GetButtonUp("Fire1"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    void LoadScene(VideoPlayer vp)
    {
        // SceneManager.LoadScene("Tutorial");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
