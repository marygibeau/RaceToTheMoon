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
    private GameObject calibrationGraphic;

    private float dimAmt = 3.0f;

    private bool targetStarBoxDimmed, starBarDimmed, timerAndScoreBoxDimmed, calibrationGraphicDimmed,
                 targetStarBoxHidden, starBarHidden, timerAndScoreBoxHidden, calibrationGraphicHidden;

    private void Awake()
    {
        starBar = this.transform.GetChild(0).gameObject;
        timerAndScoreBox = this.transform.GetChild(1).gameObject;
        targetStarBox = this.transform.GetChild(2).gameObject;
        calibrationGraphic = this.transform.GetChild(3).gameObject;
        targetStarBoxDimmed = false;
        starBarDimmed = false;
        timerAndScoreBoxDimmed = false;
        calibrationGraphicDimmed = false;
        targetStarBoxHidden = false;
        starBarHidden = false;
        timerAndScoreBoxHidden = false;
        calibrationGraphicHidden = false;
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
                    tempColor.r = tempColor.r / dimAmt;
                    tempColor.g = tempColor.g / dimAmt;
                    tempColor.b = tempColor.b / dimAmt;
                    child.gameObject.GetComponent<SpriteRenderer>().color = tempColor;
                }
                else if (child.gameObject.GetComponent<TextMeshProUGUI>() != null)
                {
                    var tempColor = child.gameObject.GetComponent<TextMeshProUGUI>().color;
                    tempColor.r = tempColor.r / dimAmt;
                    tempColor.g = tempColor.g / dimAmt;
                    tempColor.b = tempColor.b / dimAmt;
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
                    tempColor.r = tempColor.r * dimAmt;
                    tempColor.g = tempColor.g * dimAmt;
                    tempColor.b = tempColor.b * dimAmt;
                    child.gameObject.GetComponent<SpriteRenderer>().color = tempColor;
                }
                else if (child.gameObject.GetComponent<TextMeshProUGUI>() != null)
                {
                    var tempColor = child.gameObject.GetComponent<TextMeshProUGUI>().color;
                    tempColor.r = tempColor.r * dimAmt;
                    tempColor.g = tempColor.g * dimAmt;
                    tempColor.b = tempColor.b * dimAmt;
                    child.gameObject.GetComponent<TextMeshProUGUI>().color = tempColor;
                }
            }
            starBarDimmed = false;
        }
    }


    public void HideStarBar()
    {
        if (!starBarHidden)
        {
            starBar.gameObject.SetActive(false);
            starBarHidden = true;
        }
    }

    public void ShowStarBar()
    {
        if (starBarHidden)
        {
            starBar.gameObject.SetActive(true);
            starBarHidden = false;
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
            tempColor.r = tempColor.r / dimAmt;
            tempColor.g = tempColor.g / dimAmt;
            tempColor.b = tempColor.b / dimAmt;
            score.GetComponent<TextMeshProUGUI>().color = tempColor; // score text
            tempColor = score.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().color; // score title
            tempColor.r = tempColor.r / dimAmt;
            tempColor.g = tempColor.g / dimAmt;
            tempColor.b = tempColor.b / dimAmt;
            score.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().color = tempColor; // score title
                                                                                                      // dim timer and piechart graphics
            tempColor = timer.GetComponent<TextMeshProUGUI>().color; // timer text
            tempColor.r = tempColor.r / dimAmt;
            tempColor.g = tempColor.g / dimAmt;
            tempColor.b = tempColor.b / dimAmt;
            timer.GetComponent<TextMeshProUGUI>().color = tempColor; // timer text
            tempColor = timer.transform.GetChild(0).gameObject.GetComponent<Image>().color; // empty pie
            tempColor.r = tempColor.r / dimAmt;
            tempColor.g = tempColor.g / dimAmt;
            tempColor.b = tempColor.b / dimAmt;
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
            tempColor.r = tempColor.r * dimAmt;
            tempColor.g = tempColor.g * dimAmt;
            tempColor.b = tempColor.b * dimAmt;
            score.GetComponent<TextMeshProUGUI>().color = tempColor; // score text
            tempColor = score.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().color; // score title
            tempColor.r = tempColor.r * dimAmt;
            tempColor.g = tempColor.g * dimAmt;
            tempColor.b = tempColor.b * dimAmt;
            score.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().color = tempColor; // score title
                                                                                                      // dim timer and piechart graphics
            tempColor = timer.GetComponent<TextMeshProUGUI>().color; // timer text
            tempColor.r = tempColor.r * dimAmt;
            tempColor.g = tempColor.g * dimAmt;
            tempColor.b = tempColor.b * dimAmt;
            timer.GetComponent<TextMeshProUGUI>().color = tempColor; // timer text
            tempColor = timer.transform.GetChild(0).gameObject.GetComponent<Image>().color; // empty pie
            tempColor.r = tempColor.r * dimAmt;
            tempColor.g = tempColor.g * dimAmt;
            tempColor.b = tempColor.b * dimAmt;
            timer.transform.GetChild(0).gameObject.GetComponent<Image>().color = tempColor; // empty pie
            timer.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Image>().color = tempColor; // full pie
            timerAndScoreBoxDimmed = false;
        }
    }


    public void HideScoreAndTimerBox()
    {
        if (!timerAndScoreBoxHidden)
        {
            timerAndScoreBox.gameObject.SetActive(false);
            timerAndScoreBoxHidden = true;
        }
    }

    public void ShowScoreAndTimerBox()
    {
        if (timerAndScoreBoxHidden)
        {
            timerAndScoreBox.gameObject.SetActive(true);
            timerAndScoreBoxHidden = false;
        }
    }

    public void DimTargetStarBox()
    {
        if (!targetStarBoxDimmed)
        {
            var tempColor = targetStarBox.GetComponent<SpriteRenderer>().color;
            tempColor.r = tempColor.r / dimAmt;
            tempColor.g = tempColor.g / dimAmt;
            tempColor.b = tempColor.b / dimAmt;
            targetStarBox.GetComponent<SpriteRenderer>().color = tempColor;
            foreach (Transform child in targetStarBox.transform)
            {
                if (child.gameObject.GetComponent<TextMeshProUGUI>() != null)
                {
                    tempColor = child.gameObject.GetComponent<TextMeshProUGUI>().color;
                    tempColor.r = tempColor.r / dimAmt;
                    tempColor.g = tempColor.g / dimAmt;
                    tempColor.b = tempColor.b / dimAmt;
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
            tempColor.r = tempColor.r * dimAmt;
            tempColor.g = tempColor.g * dimAmt;
            tempColor.b = tempColor.b * dimAmt;
            targetStarBox.GetComponent<SpriteRenderer>().color = tempColor;
            foreach (Transform child in targetStarBox.transform)
            {
                if (child.gameObject.GetComponent<TextMeshProUGUI>() != null)
                {
                    tempColor = child.gameObject.GetComponent<TextMeshProUGUI>().color;
                    tempColor.r = tempColor.r * dimAmt;
                    tempColor.g = tempColor.g * dimAmt;
                    tempColor.b = tempColor.b * dimAmt;
                    child.gameObject.GetComponent<TextMeshProUGUI>().color = tempColor;

                }
            }
            targetStarBoxDimmed = false;
        }

    }

    public void HideTargetStarBox()
    {
        if (!targetStarBoxHidden)
        {
            targetStarBox.gameObject.SetActive(false);
            targetStarBoxHidden = true;
        }
    }

    public void ShowTargetStarBox()
    {
        if (targetStarBoxHidden)
        {
            targetStarBox.gameObject.SetActive(true);
            targetStarBoxHidden = false;
        }
    }

    public void DimCalibrationGraphic()
    {
        if (!calibrationGraphicDimmed)
        {
            var tempColor = calibrationGraphic.GetComponent<RawImage>().color;
            tempColor.r = tempColor.r / dimAmt;
            tempColor.g = tempColor.g / dimAmt;
            tempColor.b = tempColor.b / dimAmt;
            calibrationGraphic.GetComponent<RawImage>().color = tempColor;
            calibrationGraphicDimmed = true;
        }
    }

    public void UndimCalibrationGraphic()
    {
        if (calibrationGraphicDimmed)
        {
            var tempColor = calibrationGraphic.GetComponent<RawImage>().color;
            tempColor.r = tempColor.r * dimAmt;
            tempColor.g = tempColor.g * dimAmt;
            tempColor.b = tempColor.b * dimAmt;
            calibrationGraphic.GetComponent<RawImage>().color = tempColor;
            calibrationGraphicDimmed = false;
        }
    }

    public void HideCalibrationGraphic()
    {
        if (!calibrationGraphicHidden)
        {
            calibrationGraphic.gameObject.SetActive(false);
            calibrationGraphicHidden = true;
        }
    }

    public void ShowCalibrationGraphic()
    {
        if (calibrationGraphicHidden)
        {
            calibrationGraphic.gameObject.SetActive(true);
            calibrationGraphicHidden = false;
        }
    }
}
