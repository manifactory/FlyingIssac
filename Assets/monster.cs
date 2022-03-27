using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monster : MonoBehaviour
{
    private Vector3 s_position;
    private Quaternion s_rotation;

    [SerializeField]
    Vector3 moveVectorSpeed = new Vector3(0,0,0);

    [SerializeField]
    float shootInterval = 1.0f;

    private float localTimer = 0.0f;

    

    void Awake()
    {
        Debug.Log(this.gameObject.transform);
        s_position = this.transform.position;
        s_rotation = this.transform.rotation;
    }

    void OnEnable()
    {
        this.transform.SetPositionAndRotation(s_position, s_rotation);
        InvokeRepeating(this.gameObject.tag+"Update",0.01f,0.01f);
    }

    void OnDisable()
    {
        CancelInvoke();
    }

    void OnTriggerEnter2D(Collider2D o)
    {
        if(o.tag == "Bullet")
            if(o.GetComponent<bullet>().target == "Enemy")
            {
                Invoke("GetDamage",0);
                o.gameObject.SetActive(false);
            }
    }

    void GetDamage()
    {
        SetColorAndReset(Color.red, 0.3f);
    }

    void ColorReset()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }


    void SetColorAndReset(Color c,float resetDelay)
    {
        GetComponent<SpriteRenderer>().color = c;
        Invoke("ColorReset",resetDelay);
    }

//여기서 몬스터의 움직임을 구현하면 됩니다.
//(해당 몬스터 게임오브젝트의 tag)+Update로 함수명을 지정하세요.
//함수내부에 PosUpdate()를 넣는 것을 권장합니다

    void FlyUpdate()
    {
        PosUpdate();
    }

    void BlobUpdate()
    {
        localTimer += Time.deltaTime;
        if(localTimer >= shootInterval)
        {
            localTimer = 0.0f;

            for(float i=-1; i<=1; i+=0.25f)
            {
                GameObject.Find("ShootBulletWraper").GetComponent<ShootBulletWraper>()
                .ShootBullet("bullet_enemy", this.transform.position, degree: 180.0f*i);
            }
        }

        PosUpdate();
    }

    void BabyUpdate()
    {
        localTimer += Time.deltaTime;
        if(localTimer >= shootInterval)
        {
            localTimer = 0.0f;

            float shootAngle = Vector2.SignedAngle(this.transform.position, GameObject.Find("Player").transform.position);
            Debug.Log(shootAngle);
            shootAngle *= Mathf.Rad2Deg;
            GameObject.Find("ShootBulletWraper").GetComponent<ShootBulletWraper>()
            .ShootBullet("bullet_enemy", this.transform.position, degree: shootAngle + 90.0f, velo: new Vector3(0,3.0f,0));
            Debug.Log(shootAngle);
        }
        PosUpdate();
    }

    void PosUpdate()
    {
        this.transform.position += moveVectorSpeed * Time.fixedDeltaTime;

        //카메라 바깥으로 나가면 비활성화
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        if(!GeometryUtility.TestPlanesAABB(planes,this.GetComponent<CircleCollider2D>().bounds))
        {
            gameObject.SetActive(false);
        }
    }
}
