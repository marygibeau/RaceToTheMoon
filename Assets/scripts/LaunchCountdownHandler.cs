using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class LaunchCountdownHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public CrosshairMovementScriptWithTutorial crosshair;

    TextMeshProUGUI textObject;
    public bool TwoTime = false;
    public bool OneTime = false;
    public bool zeroTime = false;
    public bool launch = false;

    private void Start() {
        textObject = this.gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (TwoTime)
        {
            textObject.text = "2";
            TwoTime = false;
            Debug.Log("two time");
        }
        if (OneTime)
        {
            textObject.text = "1";
            OneTime = false;
            Debug.Log("one time");
        }
        if (launch)
        {
            crosshair.Launch();
        }
    }
}
