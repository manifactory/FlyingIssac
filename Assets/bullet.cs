using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{

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
        Setup(0.0f, 1.0f, "");
        SetSinValue(0.0f,0.0f);   
    }

    public void Setup(float _speed, float _destroyTime, string _target)
    {
        mainTimer = 0.0f;
        start_angle = this.transform.eulerAngles.z;

        speed = _speed;
        destroyTime = _destroyTime;

        target = _target;

        Debug.Log(target+" "+destroyTime);

        //Invoke(nameof(DeactiveDelay), destroyTime);
    }

    public void SetSinValue(float s_rate, float s_size)
    {
        sin_rate = s_rate;
        sin_size = s_size;
    }

    //void DeactiveDelay() => gameObject.SetActive(false);

    void OnDisable()
    {
        //TODO:여기 사라지기전 모션 추가

        ObjectPooler.ReturnToPool(gameObject);
        CancelInvoke();    // Monobehaviour에 Invoke가 있다면
    }

    // Update is called once per frame
    void Update()
    {
        mainTimer += Time.deltaTime;
        
        //움직임 처리
        this.transform.Translate(Vector3.up * speed * Time.deltaTime);
        //사인파 움직임 처리
        this.transform.rotation =  Quaternion.Euler(new Vector3(0, 0, start_angle + sin_size * Mathf.Sin(mainTimer*sin_rate+Mathf.PI/2)));
    }
}
