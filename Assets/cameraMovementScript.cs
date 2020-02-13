using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovementScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Object Camera;
    private Component[] Hitboxes; 
    void Start()
    {
        Hitboxes = GetComponents(typeof(EdgeCollider2D));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
