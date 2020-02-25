using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarClickScript : MonoBehaviour { 

    public GameObject Star = null;
    public string starName = null;
    bool canClick = true;
    public float coolDown = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

        
        if (Input.GetKey(KeyCode.Return) && canClick) {
           // Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            CircleCollider2D coll = Star.GetComponent<CircleCollider2D>();
            Vector4 reticlePosition = GameObject.Find("reticle").transform.position;

            if(coll.OverlapPoint(reticlePosition))
            {
                print(starName + " clicked");
                canClick = false;
                Invoke("CooledDown", coolDown);
            } else
            {
                print("NO STAR CLICKED");
                canClick = false;
                Invoke("CooledDown", coolDown);
            }
        }
    }

     void CooledDown()
    {
        canClick = true;
    }

    private void PrintName(GameObject star)
    {
        print(star.name);
    }
}