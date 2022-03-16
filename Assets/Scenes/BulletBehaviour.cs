using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{

    private Rigidbody2D thisRbody2d;

    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        thisRbody2d = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D o)
    {
        if(o.tag == "Player")
        {
            Debug.Log("player trigger");
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        thisRbody2d.transform.Translate(Vector2.up*speed*Time.deltaTime);
    }
}
