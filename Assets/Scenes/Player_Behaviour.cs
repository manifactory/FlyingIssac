using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Player_Behaviour : MonoBehaviour
{

    Transform thisTransform; 
    SpriteRenderer thisRender;
    Rigidbody2D thisRbody2d;

    [Header("Player value")]
    public float speed;
    public float smoothValue;

    [SerializeField]
    private Vector2 targetPos = Vector2.zero; //public is debug perpose
    private Vector2 movePos = Vector2.zero;


    // Start is called before the first frame update
    void Start()
    {
        thisTransform = GetComponent<Transform>();
        thisRender = GetComponent<SpriteRenderer>();
        thisRbody2d = GetComponent<Rigidbody2D>();
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
            targetPos += Vector2.up * speed * Time.deltaTime;
            Debug.Log("W KEy input");
        }
        if (Input.GetKey(KeyCode.A))
        {
            targetPos += Vector2.left * speed * Time.deltaTime;
            Debug.Log("A KEy input");
        }
        if (Input.GetKey(KeyCode.S))
        {
            targetPos += Vector2.down * speed * Time.deltaTime;
            Debug.Log("S KEy input");
        }
        if (Input.GetKey(KeyCode.D))
        {
            targetPos += Vector2.right * speed * Time.deltaTime;
            Debug.Log("D KEy input");
        }
    }

    void FixedUpdate()
    {
        movePos = thisRbody2d.position;
        movePos = Vector2.Lerp(movePos, targetPos, smoothValue*Time.deltaTime);
        thisRbody2d.MovePosition(targetPos);
    }
}
