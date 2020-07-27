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

    public void LoadNextLevelWithFinalInfo(int finalScore, List<string> starsFound, int secondsLeft)
    {
        int i = 0;
        foreach (string star in starsFound)
        {
            PlayerPrefs.SetString("Star_" + i, star);
            i++;
        }
        if (starsFound.Count == 12 && secondsLeft > 0)
        {
            finalScore += secondsLeft * 100;
        }
        PlayerPrefs.SetInt("finalScore", finalScore);
        PlayerPrefs.SetInt("starsFound", starsFound.Count);
        LoadNextLevel();
    }
}