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
    public TextMeshProUGUI p1;
    public TextMeshProUGUI p2;
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
        tutorialAnimator.SetBool("Ready To Start", true);
    }

    private void Update()
    {
        if (tutorialAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            if (tutorialAnimator.GetCurrentAnimatorStateInfo(0).IsName("Tutorial Intro Section 1 Animation"))
            {
                p1.color = new Color(255, 0, 0, 255);
                tutorialAnimator.SetTrigger("Paragraph 1 Done");
            }
            else if (tutorialAnimator.GetCurrentAnimatorStateInfo(0).IsName("Tutorial Intro Section 2 Animation"))
            {
                tutorialAnimator.SetTrigger("Paragraph 2 Done");
            }
        }
    }

}
