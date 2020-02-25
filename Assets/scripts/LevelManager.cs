using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void LoadLevel(string name){
		Debug.Log("Level load requested for " + name);
		SceneManager.LoadScene(name);
	}
	
	public void QuitRequest(){
		Debug.Log("quit");
		Application.Quit();
	}
	
	public void LoadNextLevel(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

    public void LoadNextLevelWithScore(int score) {
        PlayerPrefs.SetInt("finalScore", score);
        LoadNextLevel();
    }
}
