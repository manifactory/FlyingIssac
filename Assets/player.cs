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
    private Vector2 lastPos = Vector2.zero;
    private Vector2 momentum = Vector2.zero;

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

        targetPos = this.transform.position;
        movePos = targetPos;

        //플레이어 움직임 경계 설정
        s_boundary.xMin = GameObject.Find("TopLeft").transform.position.x;
        s_boundary.xMax = GameObject.Find("BottomRight").transform.position.x;
        s_boundary.yMin = GameObject.Find("BottomRight").transform.position.y;
        s_boundary.yMax = GameObject.Find("TopLeft").transform.position.y;
        

        Debug.Log(s_boundary);
    }

    void OnTriggerEnter2D(Collider2D o)
    {
        if(o.GetComponent<bullet>().target == "Player")
            thisRender.color = Color.red;
    }

    void OnTriggerExit2D(Collider2D o)
    {
        thisRender.color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        mainTimer+=Time.deltaTime;
        
        PlayerMovement();
        if (Input.GetKey(KeyCode.Space))
            ShootBullet();
        LimitBoundary(); //플레이어 움직임 제한

        this.transform.position = targetPos;

        momentum=(targetPos - lastPos)*50.0f;
        lastPos = targetPos;
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
        if(mainTimer-shootTimer>=shootInterval){
            shootTimer = mainTimer;
            
            Vector2 spawnpos =  this.transform.position + Vector3.up*SD + Vector3.left*(IPD/2) - Vector3.left*wrinkleLeft;
            wrinkleLeft = wrinkleLeft==IPD ? 0.0f : IPD;
            
            GameObject t_bullet = ObjectPooler.SpawnFromPool("bullet_player",spawnpos);
            bullet p_bullet = t_bullet.GetComponent<bullet>();
            p_bullet.Setup(new Vector3(0+momentum.x,1.0f+momentum.y,0),4.0f,"Enemy");
            //p_bullet.momentum = new Vector3(0,-5.0f,0);
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
        movePos = this.transform.position;
        movePos = Vector2.Lerp(movePos, targetPos, smoothValue*Time.deltaTime);
        //----
    }
}
