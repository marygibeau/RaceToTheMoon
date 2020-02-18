using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reticleMovementScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float offset = 0.1f;
    public Camera camera;
    public GameObject backgroundImage;

    private bool hitUp = false;
    private bool hitDown = false;
    private bool hitLeft = false;
    private bool hitRight = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // reticle movement
        if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && !hitUp)
        {
            this.transform.Translate(Vector2.up * offset);
        }
        if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && !hitDown)
        {
            this.transform.Translate(Vector2.down * offset);
        }
        if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && !hitLeft)
        {
            this.transform.Translate(Vector2.left * offset);
        }
        if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && !hitRight)
        {
            this.transform.Translate(Vector2.right * offset);
        }

        // camera movement
        if (hitUp)
        {
            camera.transform.Translate(Vector2.up * offset);
        }
        if (hitDown)
        {
            camera.transform.Translate(Vector2.down * offset);
        }
        if (hitLeft)
        {
            camera.transform.Translate(Vector2.left * offset);
        }
        if (hitRight)
        {
            camera.transform.Translate(Vector2.right * offset);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // turns on movement directions
        Debug.Log("Entered: " + other.gameObject.name);
        switch (other.gameObject.name)
        {
            case "right":
                hitRight = true;
                break;
            case "left":
                hitLeft = true;
                break;
            case "up":
                hitUp = true;
                break;
            case "down":
                hitDown = true;
                break;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // turns off movement directions
        Debug.Log("Exited: " + other.gameObject.name);
        switch (other.gameObject.name)
        {
            case "right":
                hitRight = false;
                break;
            case "left":
                hitLeft = false;
                break;
            case "up":
                hitUp = false;
                break;
            case "down":
                hitDown = false;
                break;
        }
    }

}
