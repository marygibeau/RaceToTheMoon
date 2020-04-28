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

    public void LoadCinematic2WithScoreandStars(int score, int stars)
    {
        PlayerPrefs.SetInt("finalScore", score);
        PlayerPrefs.SetInt("starsFound", stars);
        LoadLevel("Cinematic_2");
    }

    public void LoadNextLevelWithStarListAndTimeLeft(List<string> starsFound, int secondsLeft)
    {
        Debug.Log("load with star list and time activated");
        Debug.Log("time = " + secondsLeft);
        int i = 0;
        foreach(string star in starsFound) {
            PlayerPrefs.SetString("Star_" + i, star);
            i++;
        }
        PlayerPrefs.SetInt("SecondsLeft", secondsLeft);
        int score = starsFound.Count * 1000 + secondsLeft * 100;
        LoadNextLevelWithScoreandStars(score, starsFound.Count);
    }
}