using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{

    [Header("movement value")]
    public float speed;

    private float s_speed = 0.0f;
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


    public float HP = 3.0f;
    private float s_HP = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        targetPos = this.transform.position;
        movePos = targetPos;
        s_speed = speed;

        //플레이어 움직임 경계 설정
        s_boundary = GameObject.Find("LevelManager").GetComponent<LevelManager>().GetStageBoundary();

        InvokeRepeating("PlayerFixedUpdate",0,0.01f);
    }

    void OnTriggerEnter2D(Collider2D o)
    {
        
        switch(o.gameObject.tag)
        {

        case "Bullet":
        if(o.GetComponent<bullet>().target == "Player")
        {
            o.gameObject.SetActive(false);
            StartCoroutine(GetDamage(1));
        }
        break;

        case "item":
        
        break;

        default:
        break;
        }
    }


    // Update is called once per frame
    void PlayerFixedUpdate()
    {
        mainTimer+=Time.deltaTime;

        momentum = Vector2.zero;
        
        PlayerMovement();
        if (Input.GetKey(KeyCode.Space))
            ShootBullet();
        LimitBoundary();
        
        this.transform.position = targetPos;
    }

    void GetItem(string item_type)
    {

    }

    void PlayerMovement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            momentum += Vector2.up * speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            momentum += Vector2.left * speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            momentum += Vector2.down * speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            momentum += Vector2.right * speed;
        }

        if(Input.GetKey(KeyCode.LeftShift))
        {
            speed = s_speed/2.0f;
        }
        else
        {
            speed = s_speed;
        }
        targetPos += momentum * Time.fixedDeltaTime;
    }
    void LimitBoundary()
    {
        targetPos.x = Mathf.Clamp(targetPos.x,s_boundary.xMin, s_boundary.xMax);
        targetPos.y = Mathf.Clamp(targetPos.y,s_boundary.yMin, s_boundary.yMax);
    }

    void ShootBullet()
    {    
        if(mainTimer-shootTimer>=shootInterval){
            shootTimer = mainTimer;
            
            Vector2 spawnpos =  this.transform.position + Vector3.up*SD + Vector3.left*(IPD/2) - Vector3.left*wrinkleLeft;
            wrinkleLeft = wrinkleLeft==IPD ? 0.0f : IPD;
            
            GameObject.Find("ShootBulletWraper").GetComponent<ShootBulletWraper>()
            .ShootBullet("bullet_player", spawnpos, addvelo: new Vector3(momentum.x,momentum.y,0));
        }
    }

    IEnumerator GetDamage(int damage)
    {
        //넉백
        //this.transform.position += Vector3.up * damage * 0.1f;

        HP -= damage;
        if(HP<=0.0f)
        {
            // 종료 로직
        }
        else
        {
            StartCoroutine(SetColorAndReset(Color.red, 0.3f));
        }

        yield return null;
    }

    IEnumerator ColorReset(float delay)
    {
        yield return new WaitForSeconds(delay);
        GetComponent<SpriteRenderer>().color = Color.white;
    }


    IEnumerator SetColorAndReset(Color c,float resetDelay)
    {
        GetComponent<SpriteRenderer>().color = c;
        StartCoroutine(ColorReset(resetDelay));
        yield return null;
    }

    void FixedUpdate()
    {
        //---Deprecated
        movePos = this.transform.position;
        movePos = Vector2.Lerp(movePos, targetPos, smoothValue*Time.deltaTime);
        //----
    }
}
