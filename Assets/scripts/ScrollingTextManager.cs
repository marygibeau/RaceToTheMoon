using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScrollingTextManager : MonoBehaviour
{
    
    public TextMeshProUGUI TextMeshProComponent;
    public float ScrollSpeed = 0.05f;

    private TextMeshProUGUI m_cloneTextObject;

    private RectTransform m_textRectTransform;
    private string sourceText;
    private string tempText;


    void Awake()
    {
        m_textRectTransform = TextMeshProComponent.GetComponent<RectTransform>();

        m_cloneTextObject = Instantiate(TextMeshProComponent) as TextMeshProUGUI;
        RectTransform cloneRectTransform = m_cloneTextObject.GetComponent<RectTransform>();
        cloneRectTransform.SetParent(m_textRectTransform);
        cloneRectTransform.anchorMin = new Vector2(1, 0.5f);
        cloneRectTransform.anchorMax = new Vector2(1, 0.5f);
        cloneRectTransform.localPosition = new Vector2(100, 0);
        cloneRectTransform.localScale = new Vector3(1, 1, 1);
        
    }
    
    IEnumerator Start()
    {
        float width = TextMeshProComponent.preferredWidth * m_textRectTransform.lossyScale.x;        Debug.Log("The Width is: " + width);

        Vector3 startPosition = m_textRectTransform.position;

        float scrollPosition = 0;

        while (true)
        {
            // Scroll the Text across the screen by moving the RectTransform
            m_textRectTransform.position = new Vector3(-scrollPosition % width, startPosition.y, startPosition.z);
            Debug.Log(scrollPosition);

            scrollPosition += ScrollSpeed * 20 * Time.deltaTime;

            yield return null;
        }
    }
    
}
