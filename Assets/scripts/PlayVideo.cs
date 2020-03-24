using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayVideo : MonoBehaviour
{
    public GameObject videoPlayer;
    public int timeToStop;

    // Start is called before the first frame update
    void Start()
    {
        videoPlayer.SetActive(false);
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if(gameObject.tag == "Player")
        {
            videoPlayer.SetActive(true);
            Destroy(videoPlayer, timeToStop);
        }
    }
}
