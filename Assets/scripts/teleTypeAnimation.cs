using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class teleTypeAnimation : MonoBehaviour
{
    private TextMeshProUGUI skipText;
    bool doneTyping = false;
    float timeSinceDoneTyping = 0.0f;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        skipText = gameObject.GetComponent<TextMeshProUGUI>();

        yield return new WaitForEndOfFrame();
        StartCoroutine("type");
    }

    IEnumerator type()
    {
        int totalVisibleCharacters = skipText.textInfo.characterCount;
        int counter = 0;
        int visibleCount = 0;

        while (visibleCount < totalVisibleCharacters)
        {
            visibleCount = counter % (totalVisibleCharacters + 1);
            skipText.maxVisibleCharacters = visibleCount;
            counter++;
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(5.0f);
        StartCoroutine("erase");
    }

    void Update()
    {
        // if (doneTyping)
        // {
        //     timeSinceDoneTyping += 1 * Time.deltaTime;
        // }

        // if (timeSinceDoneTyping >= 5)
        // {
        //     doneTyping = false;
        //     StartCoroutine("erase");
        // }
        // Debug.Log("Time Since Done Typing: " + timeSinceDoneTyping);
        // return null;
    }

    IEnumerator erase()
    {
        int totalVisibleCharacters = skipText.textInfo.characterCount;
        // int counter = totalVisibleCharacters;
        int visibleCount = totalVisibleCharacters;

        while (visibleCount >= 0)
        {
            // visibleCount = counter % (totalVisibleCharacters + 1);
            Debug.Log("Visible Count: " + visibleCount);
            skipText.maxVisibleCharacters = visibleCount;
            visibleCount--;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
