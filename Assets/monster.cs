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

    public float HP = 5.0f;
    private float s_HP = 0.0f;

    [Header("Score field")]
    public int DeathScore = 1000;
    public int HitScore = 50;

    void Awake()
    {
        s_position = this.transform.position;
        s_rotation = this.transform.rotation;
        s_HP = HP;
    }

    void OnEnable()
    {
        this.transform.SetPositionAndRotation(s_position, s_rotation);
        InvokeRepeating(this.gameObject.tag+"Update",0.01f,0.01f);
        localTimer = 0.0f;
        HP = s_HP;
    }

    void OnDisable()
    {
        GameObject.Find("LevelManager").GetComponent<LevelManager>().mob_count_decrease();
        GameObject.Find("LevelManager").GetComponent<LevelManager>().AddScore(DeathScore);
        CancelInvoke();
    }

    void OnTriggerEnter2D(Collider2D o)
    {
        if(o.tag == "Bullet")
            if(o.GetComponent<bullet>().target == "Enemy")
            {
                if(o.gameObject.name != "bullet_pierce")
                    o.gameObject.SetActive(false);
                StartCoroutine(GetDamage(1));
            }
    }

    IEnumerator GetDamage(int damage)
    {
        //넉백
        //this.transform.position += Vector3.up * damage * 0.1f;
        GameObject.Find("LevelManager").GetComponent<LevelManager>().AddScore(HitScore);
        HP -= damage;
        if(HP<=0.0f)
        {
            this.gameObject.SetActive(false);
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

//여기서 몬스터의 움직임을 구현하면 됩니다.
//(해당 몬스터 게임오브젝트의 tag)+Update로 함수명을 지정하세요.
//함수 내부에 PosUpdate()는 마지막에 필수로 넣어야 합니다.
 
    //파리
    void FlyUpdate()
    {
        localTimer += Time.deltaTime;
        PosUpdate();
    }

    //아이작 대가리
    void BlobUpdate()
    {
        localTimer += Time.deltaTime;
        if(localTimer >= shootInterval)
        {
            localTimer = 0.0f;

            for(float i=-1; i<=1; i+=0.25f)
            {
                GameObject.Find("ShootBulletWraper").GetComponent<ShootBulletWraper>()
                .ShootBullet("bullet_enemy", this.transform.position, velo:new Vector3(0,2.0f,0),degree: 180.0f*i);
            }
        }

        PosUpdate();
    }

    //애기
    void BabyUpdate()
    {
        localTimer += Time.deltaTime;
        if(localTimer >= shootInterval)
        {
            localTimer = 0.0f;

            float shootAngle = GetAngle(this.transform.position, GameObject.Find("Player").transform.position) - 90.0f;
            GameObject.Find("ShootBulletWraper").GetComponent<ShootBulletWraper>()
            .ShootBullet("bullet_enemy", this.transform.position, degree: shootAngle, velo: new Vector3(0,1.0f,0));
        }
        PosUpdate();
    }

    //몬스트로(보스)
    int shoot_count = 0;
    void MonstroUpdate()
    {
        localTimer += Time.deltaTime;
        Debug.Log(localTimer);

        if(localTimer >= shootInterval)
        {
            Debug.Log("Shoot");
            localTimer = 0.0f;

            StartCoroutine(shotgun());
            shoot_count++;
        }

        if((localTimer >= shootInterval/4.0f) && (shoot_count>=3))
        {
            shoot_count = 0;
            for(int i=0; i<=Random.Range(3,6); i++)
            {
                float shootAngle = GetAngle(this.transform.position, GameObject.Find("Player").transform.position) - 90.0f;
                GameObject.Find("ShootBulletWraper").GetComponent<ShootBulletWraper>()
                .ShootBullet("bullet_enemy", this.transform.position, degree: shootAngle + Random.Range(-15.0f,15.0f), velo: new Vector3(0,Random.Range(1.0f,2.0f),0));
            }
        }

        PosUpdate();
    }

    IEnumerator shotgun()
    {
        float shootAngle = GetAngle(this.transform.position, GameObject.Find("Player").transform.position) - 90.0f;
        for(float i=-1; i<=1; i+=0.1f)
        {
            GameObject.Find("ShootBulletWraper").GetComponent<ShootBulletWraper>()
            .ShootBullet("bullet_enemy", this.transform.position, velo: new Vector3(0,3.0f,0), degree: shootAngle+180.0f*i);
        }
        yield return null;
    }

    IEnumerator RepidShot(int shoot_count, float repid_delay, IEnumerator pattern)
    {

        while(shoot_count > 0)
        {
            shoot_count--;

            StartCoroutine(pattern);
            
            yield return new WaitForSeconds(repid_delay);
        }

        yield return null;
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

    public static float GetAngle(Vector3 from, Vector3 to)
    {
        Vector3 gap = to - from;
        return Mathf.Atan2(gap.y, gap.x) * Mathf.Rad2Deg;
    }
}
