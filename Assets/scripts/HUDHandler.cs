using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDHandler : MonoBehaviour
{
    private GameObject targetStarBox;
    private GameObject starBar;
    private GameObject timerAndScoreBox;

    private bool targetStarBoxDimmed, starBarDimmed, timerAndScoreBoxDimmed, calibrationGraphicDimmed;

    private void Awake()
    {
        starBar = this.transform.GetChild(0).gameObject;
        timerAndScoreBox = this.transform.GetChild(1).gameObject;
        targetStarBox = this.transform.GetChild(2).gameObject;
        targetStarBoxDimmed = false;
        starBarDimmed = false;
        timerAndScoreBoxDimmed = false;
        calibrationGraphicDimmed = false;
    }

    public void HideHUD()
    {
        this.gameObject.SetActive(false);
    }

    public void DimHUD()
    {
        DimStarBar();
        DimScoreAndTimerBox();
        DimTargetStarBox();
        DimCalibrationGraphic();
    }

    public void UndimHUD()
    {
        UndimStarBar();
        UndimScoreAndTimerBox();
        UndimTargetStarBox();
        UndimCalibrationGraphic();
    }

    public void DimStarBar()
    {
        if (!starBarDimmed)
        {
            starBar.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f);
            foreach (Transform child in starBar.transform)
            {
                if (child.gameObject.GetComponent<SpriteRenderer>() != null)
                {
                    var tempColor = child.gameObject.GetComponent<SpriteRenderer>().color;
                    tempColor.r = tempColor.r / 2.0f;
                    tempColor.g = tempColor.g / 2.0f;
                    tempColor.b = tempColor.b / 2.0f;
                    child.gameObject.GetComponent<SpriteRenderer>().color = tempColor;
                }
                else if (child.gameObject.GetComponent<TextMeshProUGUI>() != null)
                {
                    var tempColor = child.gameObject.GetComponent<TextMeshProUGUI>().color;
                    tempColor.r = tempColor.r / 2.0f;
                    tempColor.g = tempColor.g / 2.0f;
                    tempColor.b = tempColor.b / 2.0f;
                    child.gameObject.GetComponent<TextMeshProUGUI>().color = tempColor;
                }
            }
            starBarDimmed = true;
        }
    }

    public void UndimStarBar()
    {
        if (starBarDimmed)
        {
            starBar.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f);
            foreach (Transform child in starBar.transform)
            {
                if (child.gameObject.GetComponent<SpriteRenderer>() != null)
                {
                    var tempColor = child.gameObject.GetComponent<SpriteRenderer>().color;
                    tempColor.r = tempColor.r * 2.0f;
                    tempColor.g = tempColor.g * 2.0f;
                    tempColor.b = tempColor.b * 2.0f;
                    child.gameObject.GetComponent<SpriteRenderer>().color = tempColor;
                }
                else if (child.gameObject.GetComponent<TextMeshProUGUI>() != null)
                {
                    var tempColor = child.gameObject.GetComponent<TextMeshProUGUI>().color;
                    tempColor.r = tempColor.r * 2.0f;
                    tempColor.g = tempColor.g * 2.0f;
                    tempColor.b = tempColor.b * 2.0f;
                    child.gameObject.GetComponent<TextMeshProUGUI>().color = tempColor;
                }
            }
            starBarDimmed = false;
        }
    }

    public void DimScoreAndTimerBox()
    {
        if (!timerAndScoreBoxDimmed)
        {
            // dim background box
            timerAndScoreBox.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f); // background
            GameObject score = timerAndScoreBox.transform.GetChild(0).gameObject;
            GameObject timer = timerAndScoreBox.transform.GetChild(1).gameObject;
            // dim score and score title
            var tempColor = score.GetComponent<TextMeshProUGUI>().color; // score text
            tempColor.r = tempColor.r / 2.0f;
            tempColor.g = tempColor.g / 2.0f;
            tempColor.b = tempColor.b / 2.0f;
            score.GetComponent<TextMeshProUGUI>().color = tempColor; // score text
            tempColor = score.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().color; // score title
            tempColor.r = tempColor.r / 2.0f;
            tempColor.g = tempColor.g / 2.0f;
            tempColor.b = tempColor.b / 2.0f;
            score.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().color = tempColor; // score title
                                                                                                      // dim timer and piechart graphics
            tempColor = timer.GetComponent<TextMeshProUGUI>().color; // timer text
            tempColor.r = tempColor.r / 2.0f;
            tempColor.g = tempColor.g / 2.0f;
            tempColor.b = tempColor.b / 2.0f;
            timer.GetComponent<TextMeshProUGUI>().color = tempColor; // timer text
            tempColor = timer.transform.GetChild(0).gameObject.GetComponent<Image>().color; // empty pie
            tempColor.r = tempColor.r / 2.0f;
            tempColor.g = tempColor.g / 2.0f;
            tempColor.b = tempColor.b / 2.0f;
            timer.transform.GetChild(0).gameObject.GetComponent<Image>().color = tempColor; // empty pie
            timer.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Image>().color = tempColor; // full pie
            timerAndScoreBoxDimmed = true;
        }
    }

    public void UndimScoreAndTimerBox()
    {
        if (timerAndScoreBoxDimmed)
        {
            // dim background box
            timerAndScoreBox.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f); // background
            GameObject score = timerAndScoreBox.transform.GetChild(0).gameObject;
            GameObject timer = timerAndScoreBox.transform.GetChild(1).gameObject;
            // dim score and score title
            var tempColor = score.GetComponent<TextMeshProUGUI>().color; // score text
            tempColor.r = tempColor.r * 2.0f;
            tempColor.g = tempColor.g * 2.0f;
            tempColor.b = tempColor.b * 2.0f;
            score.GetComponent<TextMeshProUGUI>().color = tempColor; // score text
            tempColor = score.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().color; // score title
            tempColor.r = tempColor.r * 2.0f;
            tempColor.g = tempColor.g * 2.0f;
            tempColor.b = tempColor.b * 2.0f;
            score.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().color = tempColor; // score title
                                                                                                      // dim timer and piechart graphics
            tempColor = timer.GetComponent<TextMeshProUGUI>().color; // timer text
            tempColor.r = tempColor.r * 2.0f;
            tempColor.g = tempColor.g * 2.0f;
            tempColor.b = tempColor.b * 2.0f;
            timer.GetComponent<TextMeshProUGUI>().color = tempColor; // timer text
            tempColor = timer.transform.GetChild(0).gameObject.GetComponent<Image>().color; // empty pie
            tempColor.r = tempColor.r * 2.0f;
            tempColor.g = tempColor.g * 2.0f;
            tempColor.b = tempColor.b * 2.0f;
            timer.transform.GetChild(0).gameObject.GetComponent<Image>().color = tempColor; // empty pie
            timer.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Image>().color = tempColor; // full pie
            timerAndScoreBoxDimmed = false;
        }
    }

    public void DimTargetStarBox()
    {
        if (!targetStarBoxDimmed)
        {
            var tempColor = targetStarBox.GetComponent<SpriteRenderer>().color;
            tempColor.r = tempColor.r / 2.0f;
            tempColor.g = tempColor.g / 2.0f;
            tempColor.b = tempColor.b / 2.0f;
            targetStarBox.GetComponent<SpriteRenderer>().color = tempColor;
            foreach (Transform child in targetStarBox.transform)
            {
                if (child.gameObject.GetComponent<TextMeshProUGUI>() != null)
                {
                    tempColor = child.gameObject.GetComponent<TextMeshProUGUI>().color;
                    tempColor.r = tempColor.r / 2.0f;
                    tempColor.g = tempColor.g / 2.0f;
                    tempColor.b = tempColor.b / 2.0f;
                    child.gameObject.GetComponent<TextMeshProUGUI>().color = tempColor;

                }
            }
            targetStarBoxDimmed = true;
        }
    }

    public void UndimTargetStarBox()
    {
        if (targetStarBoxDimmed)
        {
            var tempColor = targetStarBox.GetComponent<SpriteRenderer>().color;
            tempColor.r = tempColor.r * 2.0f;
            tempColor.g = tempColor.g * 2.0f;
            tempColor.b = tempColor.b * 2.0f;
            targetStarBox.GetComponent<SpriteRenderer>().color = tempColor;
            foreach (Transform child in targetStarBox.transform)
            {
                if (child.gameObject.GetComponent<TextMeshProUGUI>() != null)
                {
                    tempColor = child.gameObject.GetComponent<TextMeshProUGUI>().color;
                    tempColor.r = tempColor.r * 2.0f;
                    tempColor.g = tempColor.g * 2.0f;
                    tempColor.b = tempColor.b * 2.0f;
                    child.gameObject.GetComponent<TextMeshProUGUI>().color = tempColor;

                }
            }
            targetStarBoxDimmed = false;
        }

    }

    public void DimCalibrationGraphic()
    {
        if (!calibrationGraphicDimmed)
        {
            // TODO
            Debug.Log("Normally, I'd dim the calibration grpahic here");
            calibrationGraphicDimmed = true;
        }
    }

    public void UndimCalibrationGraphic()
    {
        if (calibrationGraphicDimmed)
        {
            // TODO
            Debug.Log("Normally, I'd undim the calibration grpahic here");
            calibrationGraphicDimmed = false;
        }
    }
}
