using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialTypewriterAnimation : MonoBehaviour
{
    float writeSpeed = 0.025f;
    public TextMeshProUGUI scoreInfoText;
    public TextMeshProUGUI targetInfoText;
    public TextMeshProUGUI timerInfoText;
    public Animator tutorialAnimator;

    IEnumerator Start()
    {
        scoreInfoText.maxVisibleCharacters = 0;
        targetInfoText.maxVisibleCharacters = 0;
        timerInfoText.maxVisibleCharacters = 0;
        tutorialAnimator = gameObject.GetComponent<Animator>();
        yield return new WaitForEndOfFrame();
    }

    IEnumerator scoreType()
    {
        int totalVisibleCharacters = scoreInfoText.textInfo.characterCount;
        int counter = 0;
        int visibleCount = 0;

        while (visibleCount < totalVisibleCharacters)
        {
            visibleCount = counter % (totalVisibleCharacters + 1);
            scoreInfoText.maxVisibleCharacters = visibleCount;
            counter++;
            yield return new WaitForSeconds(writeSpeed);
        }
        tutorialAnimator.SetTrigger("Score Done");
    }

    IEnumerator targetType()
    {
        int totalVisibleCharacters = targetInfoText.textInfo.characterCount;
        int counter = 0;
        int visibleCount = 0;

        while (visibleCount < totalVisibleCharacters)
        {
            visibleCount = counter % (totalVisibleCharacters + 1);
            targetInfoText.maxVisibleCharacters = visibleCount;
            counter++;
            yield return new WaitForSeconds(writeSpeed);
        }
        tutorialAnimator.SetTrigger("Target Done");
    }

    IEnumerator timerType()
    {
        int totalVisibleCharacters = timerInfoText.textInfo.characterCount;
        int counter = 0;
        int visibleCount = 0;

        while (visibleCount < totalVisibleCharacters)
        {
            visibleCount = counter % (totalVisibleCharacters + 1);
            timerInfoText.maxVisibleCharacters = visibleCount;
            counter++;
            yield return new WaitForSeconds(writeSpeed);
        }
    }

}
