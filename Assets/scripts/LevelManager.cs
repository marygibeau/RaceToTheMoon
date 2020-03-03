using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;



public class LevelManager : MonoBehaviour
{
    // This method loads the scene with the name you pass in
    public void LoadLevel(string name)
    {
        Debug.Log("Level load requested for " + name);
        SceneManager.LoadScene(name);
    }

    // This loads the next level in the build settings
    // make sure all scenes are in the build settings and in order	
    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // This just to pass the score to the final menu
    public void LoadNextLevelWithScoreandStars(int score, int stars)
    {
        PlayerPrefs.SetInt("finalScore", score);
        PlayerPrefs.SetInt("starsFound", stars);
        LoadNextLevel();
    }
}