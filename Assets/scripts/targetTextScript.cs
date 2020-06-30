using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetTextScript : MonoBehaviour
{
    Text textObject;
    // Start is called before the first frame update
    void Start()
    {
        textObject = this.GetComponent<Text>();
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
