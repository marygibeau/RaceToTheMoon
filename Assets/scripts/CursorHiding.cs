using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorHiding : MonoBehaviour
{
    LevelManager lvlr;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        lvlr = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape)) {
            Application.Quit();
        }
    }
}
