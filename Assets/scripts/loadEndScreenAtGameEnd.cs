using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class loadEndScreenAtGameEnd : MonoBehaviour
{
    public VideoPlayer VideoPlayer; // Drag & Drop the GameObject holding the VideoPlayer component
    public LevelManager lvlr;

    void Start()
    {
        VideoPlayer.loopPointReached += LoadScene;
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            int currentScore = PlayerPrefs.GetInt("finalScore");
            int starsFound = PlayerPrefs.GetInt("starsFound");
            lvlr.LoadNextLevelWithScoreandStars(currentScore, starsFound);
        }
    }
    void LoadScene(VideoPlayer vp)
    {
        int currentScore = PlayerPrefs.GetInt("finalScore");
        int starsFound = PlayerPrefs.GetInt("starsFound");
        lvlr.LoadNextLevelWithScoreandStars(currentScore, starsFound);
    }
}
