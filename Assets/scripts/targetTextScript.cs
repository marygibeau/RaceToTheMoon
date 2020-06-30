using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TargetTextScript : MonoBehaviour
{
    TextMeshProUGUI textObject;
    // Start is called before the first frame update
    void Start()
    {
        textObject = this.GetComponent<TextMeshProUGUI>();
    }

    public void GenerateTargetText(string target)
    {
        if (target == "done")
        {
            textObject.text = "Target: ";
        }
        else
        {
            textObject.text = "Target: " + target + " ";
        }
    }
}
