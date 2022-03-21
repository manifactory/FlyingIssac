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

    [Header("shoot value")]
    public float shootInterval = 0.0f;
    private float shootTimer = 0.0f;
    
    public float IPD = 0.5f;
    public float SD = 0.2f;
    private float wrinkleLeft = 0.0f;

    private Rect s_boundary;

    // Start is called before the first frame update
    void Start()
    {
        thisRender = GetComponent<SpriteRenderer>();
        thisRbody2d = GetComponent<Rigidbody2D>();

        targetPos = this.transform.position;
        movePos = targetPos;

        //static
        s_boundary.xMin = GameObject.Find("TopLeft").transform.position.x;
        s_boundary.xMax = GameObject.Find("BottomRight").transform.position.x;
        s_boundary.yMin = GameObject.Find("BottomRight").transform.position.y;
        s_boundary.yMax = GameObject.Find("TopLeft").transform.position.y;
        

        Debug.Log(s_boundary);
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

        PlayerMovement();
        ShootBullet();
        LimitBoundary();
        thisRbody2d.MovePosition(targetPos);
    }

    void PlayerMovement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            targetPos += Vector2.up * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            targetPos += Vector2.left * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            targetPos += Vector2.down * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            targetPos += Vector2.right * speed * Time.deltaTime;
        }
    }

    void ShootBullet()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            
            if(mainTimer-shootTimer>=shootInterval){
                shootTimer = mainTimer;
                
                GameObject t_bullet = ObjectPooler.SpawnFromPool("bullet", thisRbody2d.position + Vector2.up*SD + Vector2.left*(IPD/2) - Vector2.left*wrinkleLeft);
                wrinkleLeft = wrinkleLeft==IPD ? 0.0f : IPD;

                //발사 각도 지정 (굳이 필요 없을수도)
                t_bullet.transform.rotation = Quaternion.Euler(0,0,0);
            }
        }
    }

    void LimitBoundary()
    {
        targetPos.x = Mathf.Clamp(targetPos.x,s_boundary.xMin, s_boundary.xMax);
        targetPos.y = Mathf.Clamp(targetPos.y,s_boundary.yMin, s_boundary.yMax);
    }

    void FixedUpdate()
    {
        //---Deprecated
        movePos = thisRbody2d.position;
        movePos = Vector2.Lerp(movePos, targetPos, smoothValue*Time.deltaTime);
        //----

        
    }
}
