using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Behaviour : MonoBehaviour
{

    Transform local_pos; 
    SpriteRenderer thisRender;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        local_pos = GetComponent<Transform>();
        thisRender = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D o)
    {
        thisRender.color = Color.green;
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
        if (Input.GetKey(KeyCode.W))
        {
            local_pos.position += Vector3.up * speed * Time.deltaTime;
            Debug.Log("W KEy input");
        }
        if (Input.GetKey(KeyCode.A))
        {
            local_pos.position += Vector3.left * speed * Time.deltaTime;
            Debug.Log("A KEy input");
        }
        if (Input.GetKey(KeyCode.S))
        {
            local_pos.position += Vector3.down * speed * Time.deltaTime;
            Debug.Log("S KEy input");
        }
        if (Input.GetKey(KeyCode.D))
        {
            local_pos.position += Vector3.right * speed * Time.deltaTime;
            Debug.Log("D KEy input");
        }
    }
}
