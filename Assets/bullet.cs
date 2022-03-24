using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{

    private Transform start_transform;

    public Vector3 velocity;
    public Vector3 momentum;
    public float destroyTime = 1.0f;
    private float mainTimer = 0.0f;


    public float sin_rate = 0.0f;
    public float sin_size = 0.0f;

    public string target = "";

    // Start is called before the first frame update
    void OnEnable()
    {
        //init
        start_transform = this.transform;
        Setup(new Vector3(0,0,0), 1.0f, "");
        SetSinValue(0.0f,0.0f);
        
        //start update coroutine
        InvokeRepeating("BulletFixedUpdate",0,0.01f);
    }
    void OnDisable()
    {
        //TODO:여기 사라지기전 모션 추가

        ObjectPooler.ReturnToPool(gameObject);
        CancelInvoke();    // Monobehaviour에 Invoke가 있다면
    }

    public void Setup(Vector3 _velo, float _destroyTime, string _target)
    {
        mainTimer = 0.0f;

        velocity = _velo;
        destroyTime = _destroyTime;

        target = _target;
    }

    public void SetSinValue(float s_rate, float s_size)
    {
        sin_rate = s_rate;
        sin_size = s_size;
    }

    void BulletFixedUpdate()
    {
        mainTimer += Time.deltaTime;

        if(mainTimer >= destroyTime)
        {
            gameObject.SetActive(false);
        }

        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        if(!GeometryUtility.TestPlanesAABB(planes,this.GetComponent<CircleCollider2D>().bounds))
        {
            gameObject.SetActive(false);
        }

        velocity += momentum * Time.deltaTime;
        this.transform.Translate(velocity*Time.fixedDeltaTime);
        //this.transform.eulerAngles = start_transform.eulerAngles + new Vector3(0, 0, mainTimer);
    }
}
