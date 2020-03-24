using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.UI;

public class targetTextScript : MonoBehaviour
{
    Text textObject;
    TargetStar targetStar;
    // Start is called before the first frame update
    void Start()
    {
        textObject = this.GetComponent<Text>();
        targetStar = GameObject.Find("TargetStarHandler").GetComponent<TargetStar>();
        GenerateTargetText();
    }

    // Update is called once per frame
    void Update()
    {
        GenerateTargetText();
    }

    void GenerateTargetText() {
        textObject.text = "Target: " + targetStar.GetTarget() + " ";
    }
}
