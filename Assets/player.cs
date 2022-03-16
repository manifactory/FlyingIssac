using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{

    SpriteRenderer thisRender;
    Rigidbody2D thisRbody2d;

    [Header("movement value")]
    public float speed;
    public float smoothValue;

    [SerializeField]
    private Vector2 targetPos = Vector2.zero;
    private Vector2 movePos = Vector2.zero;

    private float mainTimer = 0.0f;

    //shoot value
    public float shootInterval = 0.0f;
    private float shootTimer = 0.0f;
    


    // Start is called before the first frame update
    void Start()
    {
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
        mainTimer+=Time.deltaTime;
        if (Input.GetKey(KeyCode.W))
        {
            targetPos += Vector2.up * speed * Time.deltaTime;
            Debug.Log("W key input");
        }
        if (Input.GetKey(KeyCode.A))
        {
            targetPos += Vector2.left * speed * Time.deltaTime;
            Debug.Log("A key input");
        }
        if (Input.GetKey(KeyCode.S))
        {
            targetPos += Vector2.down * speed * Time.deltaTime;
            Debug.Log("S key input");
        }
        if (Input.GetKey(KeyCode.D))
        {
            targetPos += Vector2.right * speed * Time.deltaTime;
            Debug.Log("D key input");
        }

        if (Input.GetKey(KeyCode.Space))
        {
            
            if(mainTimer-shootTimer>=shootInterval){
                Debug.Log("a");
                shootTimer = mainTimer;
                GameObject t_bullet = PoolingManager.instance.GetQueue();
                t_bullet.transform.position = thisRbody2d.position + Vector2.up;
            }
        }
    }

    void FixedUpdate()
    {
        movePos = thisRbody2d.position;
        movePos = Vector2.Lerp(movePos, targetPos, smoothValue*Time.deltaTime);
        //
        thisRbody2d.MovePosition(targetPos);
    }
}
