using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalibrationGraphicHandler : MonoBehaviour
{
    public UnityEngine.Video.VideoPlayer calibrationVideoPlayer;
    public UnityEngine.Video.VideoClip[] calibrationClips = new UnityEngine.Video.VideoClip[25];
    private int currentClip = 0;
    private bool inTransition = false;

    // Start is called before the first frame update
    void Start()
    {
        calibrationVideoPlayer.clip = calibrationClips[0];
    }

    public void UpdateGraphic()
    {
        if (currentClip < calibrationClips.Length - 2)
        {
            Debug.Log("UPDATING GRAPHIC TO CLIP #" + (currentClip + 1));
            calibrationVideoPlayer.loopPointReached += StartTransition;
        }
    }

    private void StartTransition(UnityEngine.Video.VideoPlayer vp)
    {
        calibrationVideoPlayer.loopPointReached -= StartTransition;
        inTransition = (++currentClip) % 2 == 1;
        calibrationVideoPlayer.clip = calibrationClips[currentClip];
        calibrationVideoPlayer.isLooping = false;
        calibrationVideoPlayer.loopPointReached += EndTransition;
    }

    private void EndTransition(UnityEngine.Video.VideoPlayer vp)
    {
        Debug.Log("UPDATED GRAPHIC TO CLIP #" + (currentClip + 1));
        inTransition = (++currentClip) % 2 == 1;
        vp.clip = calibrationClips[currentClip];
        vp.isLooping = true;
        vp.loopPointReached -= EndTransition;
    }
}
