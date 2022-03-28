using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{

    [Header("movement value")]
    public float speed;

    private float s_speed = 0.0f;
    public float smoothValue;

    public Vector2 targetPos = Vector2.zero;
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

    public string bullet_type = "bullet_player";
    private bool three_eye = false;

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
            Debug.Log("get damage");
            StartCoroutine(GetDamage(1));
        }
        break;

        case "Item":
        Debug.Log("get item");
        GetItem(o.gameObject.name);
        o.gameObject.SetActive(false);
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
        switch(item_type)
        {
            case "Arrow":
                bullet_type = "bullet_pierce";
                GameObject.Find("Canvas").GetComponent<UI_System>()
                .SetImageVisible("Itemslot_1",1);
                break;
            
            case "Eye":
                three_eye = true;
                GameObject.Find("Canvas").GetComponent<UI_System>()
                .SetImageVisible("Itemslot_2",1);
                break;
            
            default:
                break;
        }
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
            if(three_eye)
            {
                GameObject.Find("ShootBulletWraper").GetComponent<ShootBulletWraper>()
            .ShootBullet(bullet_type, this.transform.position + Vector3.up*SD + Vector3.left * 0.2f, velo: new Vector3(momentum.x,momentum.y+5.0f,0));
                GameObject.Find("ShootBulletWraper").GetComponent<ShootBulletWraper>()
            .ShootBullet(bullet_type, this.transform.position + Vector3.up*SD + Vector3.right * 0.2f, velo: new Vector3(momentum.x,momentum.y+5.0f,0));
            GameObject.Find("ShootBulletWraper").GetComponent<ShootBulletWraper>()
            .ShootBullet(bullet_type, this.transform.position + Vector3.up*SD + Vector3.up * 0.2f, velo: new Vector3(momentum.x,momentum.y+5.0f,0));
            }
            else
            {
                Vector2 spawnpos =  this.transform.position + Vector3.up*SD + Vector3.left*(IPD/2) - Vector3.left*wrinkleLeft;
                wrinkleLeft = wrinkleLeft==IPD ? 0.0f : IPD;

                GameObject.Find("ShootBulletWraper").GetComponent<ShootBulletWraper>()
            .ShootBullet(bullet_type, spawnpos, velo: new Vector3(momentum.x,momentum.y+5.0f,0));
                
            }
            
            

            
            
            
        }
    }

    IEnumerator GetDamage(int damage)
    {
        //넉백
        //this.transform.position += Vector3.up * damage * 0.1f;

        StartCoroutine(SetColorAndReset(Color.red, 0.3f));

        // HP -= damage;
        // if(HP<=0.0f)
        // {
        //     this.gameObject.SetActive(false);
        // }
        // else
        // {
        //     StartCoroutine(SetColorAndReset(Color.red, 0.3f));
        // }
        yield return null;
    }

    IEnumerator SetChildColor(Color c)
    {
        //Debug.Log("setColor");
        for(int i=0;i<this.transform.childCount;i++)
        {
            //Debug.Log(this.transform.GetChild(i).name);
            SpriteRenderer s_renderer;
            if(this.transform.GetChild(i).TryGetComponent<SpriteRenderer>(out s_renderer)
                &&this.transform.GetChild(i).name != "Shadow")
            {
                s_renderer.color = c;
            }
        }
        GetComponent<SpriteRenderer>().color = c;
        yield return null;
    }

    IEnumerator ResetChildColor(float delay)
    {
        yield return new WaitForSeconds(delay);
        StartCoroutine(SetChildColor(Color.white));
    }


    IEnumerator SetColorAndReset(Color c,float resetDelay)
    {
        StartCoroutine(SetChildColor(c));
        StartCoroutine(ResetChildColor(resetDelay));
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
