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

    private void Start()
    {
        starBar = this.transform.GetChild(0).gameObject;
        timerAndScoreBox = this.transform.GetChild(1).gameObject;
        targetStarBox = this.transform.GetChild(2).gameObject;
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
    }

    public void DimStarBar()
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
    }

    public void DimScoreAndTimerBox()
    {
        // dim background box
        timerAndScoreBox.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f);
        GameObject score = timerAndScoreBox.transform.GetChild(0).gameObject;
        GameObject timer = timerAndScoreBox.transform.GetChild(1).gameObject;
        // dim score and score title
        score.GetComponent<TextMeshProUGUI>().color = new Color(0.5f, 0.5f, 0.5f);
        score.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().color = new Color(0.5f, 0.5f, 0.5f);
        // dim timer and piechart graphics
        timer.GetComponent<TextMeshProUGUI>().color = new Color(0.5f, 0.5f, 0.5f);
        var tempColor = timer.transform.GetChild(0).gameObject.GetComponent<Image>().color;
        tempColor.r = tempColor.r / 2.0f;
        tempColor.g = tempColor.g / 2.0f;
        tempColor.b = tempColor.b / 2.0f;
        timer.transform.GetChild(0).gameObject.GetComponent<Image>().color = tempColor;
        timer.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Image>().color = tempColor;
    }

    public void DimTargetStarBox()
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
    }
}
