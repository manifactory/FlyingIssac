using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public bullet inst;
    private Collider2D t_col2d = null;

    public float speed = 0;
    public float destroyTime = 1.0f;
    private float mainTimer = 0.0f;


    private float init_rotation = 0.0f;
    public float sin_rate = 0.0f;
    public float sin_size = 0.0f;
    
    public string shoot_target = "Enemy";

    // Start is called before the first frame update
    void OnEnable()
    {
        inst = this;
        //init
        mainTimer = 0.0f;
        t_col2d = null;

        init_rotation = this.transform.eulerAngles.z;

        speed = 0;
        sin_rate = 0;
        sin_size = 0;
        shoot_target = null;

        Invoke(nameof(DeactiveDelay), destroyTime);
    }

    void DeactiveDelay() => gameObject.SetActive(false);

    // void Setup()
    // {
        //setup code here
    // }

    void OnDisable()
    {
        //TODO:여기 사라지기전 모션 추가

        ObjectPooler.ReturnToPool(gameObject);
        //CancelInvoke();    // Monobehaviour에 Invoke가 있다면
    }

    void OnTriggerEnter2D(Collider2D o)
    {
        t_col2d = o;
    }

    // Update is called once per frame
    void Update()
    {
        mainTimer += Time.deltaTime;

        //Disable 조건
        if((t_col2d != null)&&(t_col2d.tag==shoot_target))
            gameObject.SetActive(false);
        
        //움직임 처리
        this.transform.Translate(Vector3.up * speed * Time.deltaTime);
        //사인파 움직임 처리
        this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, init_rotation+sin_size * Mathf.Sin(mainTimer*sin_rate+Mathf.PI/2)));
    }
}
