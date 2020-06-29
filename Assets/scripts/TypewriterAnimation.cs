using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypewriterAnimation : MonoBehaviour
{

    public float writeSpeed = 0.05f;
    public float eraseSpeed = 0.02f;
    public float displayTime = 5.0f;
    private TextMeshProUGUI skipText;
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
            yield return new WaitForSeconds(writeSpeed);
        }
        yield return new WaitForSeconds(displayTime);
        StartCoroutine("erase");
    }

    IEnumerator erase()
    {
        int totalVisibleCharacters = skipText.textInfo.characterCount;
        int visibleCount = totalVisibleCharacters;

        while (visibleCount >= 0)
        {
            skipText.maxVisibleCharacters = visibleCount;
            visibleCount--;
            yield return new WaitForSeconds(eraseSpeed);
        }
    }
}
