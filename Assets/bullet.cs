using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{

    private Transform start_transform;

    public float speed = 0;
    public float destroyTime = 1.0f;
    private float mainTimer = 0.0f;


    private float start_angle = 0.0f;
    public float sin_rate = 0.0f;
    public float sin_size = 0.0f;

    public string target = "";

    // Start is called before the first frame update
    void OnEnable()
    {
        //init
        start_transform = this.transform;
        Setup(0.0f, 1.0f, "");
        SetSinValue(0.0f,0.0f);   
    }

    void OnDisable()
    {
        //TODO:여기 사라지기전 모션 추가

        ObjectPooler.ReturnToPool(gameObject);
        CancelInvoke();    // Monobehaviour에 Invoke가 있다면
    }

    public void Setup(float _speed, float _destroyTime, string _target)
    {
        mainTimer = 0.0f;
        start_angle = this.transform.eulerAngles.z;

        speed = _speed;
        destroyTime = _destroyTime;

        target = _target;
    }

    public void SetSinValue(float s_rate, float s_size)
    {
        sin_rate = s_rate;
        sin_size = s_size;
    }

    

    // Update is called once per frame
    void Update()
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

        //움직임 처리
        this.transform.eulerAngles = new Vector3(0, 0, start_angle + sin_size * Mathf.Sin(mainTimer*sin_rate+Mathf.PI/2));
        this.transform.Translate(0,speed * Time.deltaTime,0);
        
    }
}
