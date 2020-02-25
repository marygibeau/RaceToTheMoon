using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour
{
    public float offset = 0.1f;
    private bool upBoundary = false;
    private bool downBoundary = false;
    private bool leftBoundary = false;
    private bool rightBoundary = false;

    // methods to translate camera until boundary is reached
    public void moveRight()
    {
        if (!rightBoundary)
        {
            this.transform.Translate(Vector2.right * offset);
        }
    }

    public void moveLeft()
    {
        if (!leftBoundary)
        {
            this.transform.Translate(Vector2.left * offset);
        }
    }
    public void moveUp()
    {
        if (!upBoundary)
        {
            this.transform.Translate(Vector2.up * offset);
        }
    }

    public void moveDown()
    {
        if (!downBoundary)
        {
            this.transform.Translate(Vector2.down * offset);
        }
    }
    // checks if hit boundary
    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.name)
        {
            case "rightBoundary":
                rightBoundary = true;
                break;
            case "leftBoundary":
                leftBoundary = true;
                break;
            case "upBoundary":
                upBoundary = true;
                break;
            case "downBoundary":
                downBoundary = true;
                break;
        }
    }

    // checks if no longer hitting boundary
    void OnTriggerExit2D(Collider2D other)
    {
        switch (other.gameObject.name)
        {
            case "rightBoundary":
                rightBoundary = false;
                break;
            case "leftBoundary":
                leftBoundary = false;
                break;
            case "upBoundary":
                upBoundary = false;
                break;
            case "downBoundary":
                downBoundary = false;
                break;
        }
    }
}
