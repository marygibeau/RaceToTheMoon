using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarBarController : MonoBehaviour
{
    // GameObject[] activeStars;
    public Texture[] statusBars; 
    GameObject starBarImage;
    // Start is called before the first frame update
    void Start()
    {
        starBarImage = this.transform.GetChild(0).gameObject;
        starBarImage.GetComponent<RawImage>().texture = statusBars[0];
    }

    public void UpdateStarBar(int starsCollected)
    {
        starBarImage.GetComponent<RawImage>().texture = statusBars[starsCollected];
    }
}
