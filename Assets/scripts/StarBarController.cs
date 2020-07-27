using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarBarController : MonoBehaviour
{
    GameObject[] activeStars;
    // Start is called before the first frame update
    void Start()
    {
        activeStars = new GameObject[12];
        int i = 0;
        foreach (Transform child in transform)
        {
            if (i < 12)
            {
                activeStars[i] = child.gameObject.transform.GetChild(0).gameObject;
                i++;
            }
        }
    }

    public void UpdateStarBar(int starsCollected)
    {
        activeStars[starsCollected - 1].SetActive(true);
    }
}
