using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    
    public Vector3 velocity = Vector3.zero;
    private Vector3 s_velocity;
    public float destroyTime = 1.0f;
    private float s_destroyTime;
    public string target = "";
    private string s_target;

    private float mainTimer = 0.0f;

    void Awake()
    {
       s_velocity = velocity;
       s_destroyTime = destroyTime;
       s_target = target;

       Debug.Log(s_velocity+"\n"+s_destroyTime+"\n"+s_target);
    }

    
    void OnEnable()
    {
        Setup(s_velocity, s_destroyTime, s_target);

        InvokeRepeating("BulletFixedUpdate",0,0.01f);
    }
    void OnDisable()
    {
        //TODO:여기 사라지기전 모션 추가
        ObjectPooler.ReturnToPool(gameObject);
        CancelInvoke();    // Monobehaviour에 Invoke가 있다면
    }

    public void Setup(Vector3 v, float dt, string t)
    {
        mainTimer = 0.0f;

        velocity = v;
        destroyTime = dt;
        target = t;
    }

    void BulletFixedUpdate()
    {
        mainTimer += Time.deltaTime;
        
        if((mainTimer >= destroyTime) && (destroyTime != 0))
        {
            gameObject.SetActive(false);
        }

        //카메라 경계 밖으로 나가면 컬링
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        if(!GeometryUtility.TestPlanesAABB(planes,this.GetComponent<CircleCollider2D>().bounds))
        {
            gameObject.SetActive(false);
        }

        this.transform.transform.Translate(velocity*Time.fixedDeltaTime);
    }

    public Vector3 getStaticVelocity()
    {
        return velocity;
    }
    public float getStaticDestroyTime()
    {
        return destroyTime;
    }
    public string getStaticTarget()
    {
        return target;
    }
    
}
