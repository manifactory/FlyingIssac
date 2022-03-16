using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle_Behavior : MonoBehaviour
{

    SpriteRenderer thisRender;
    Rigidbody2D thisRbody2d;

    // Start is called before the first frame update
    void Start()
    {
        thisRender = GetComponent<SpriteRenderer>();
        thisRbody2d = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D o)
    {
        thisRender.color = Color.red;
        Debug.Log("stay");
    }

    void OnTriggerExit2D(Collider2D o)
    {
        thisRender.color = Color.white;
        Debug.Log("exit");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
