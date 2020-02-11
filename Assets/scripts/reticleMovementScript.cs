using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reticleMovementScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float offset = 0.1f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) {
            this.transform.Translate(Vector2.up * offset);
        }
        if(Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) {
            this.transform.Translate(Vector2.down * offset);
        }
        if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
            this.transform.Translate(Vector2.left * offset);
        }
        if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
            this.transform.Translate(Vector2.right * offset);
        }
    }
}
