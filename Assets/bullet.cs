using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{

    private Transform thisTrans = null;

    public float speed = 0;
    public float sin_rate = 0.0f;
    public float sin_size = 0.0f;

    private Collider2D t_Col2d = null;

    public float destroyInterval = 1.0f;
    public float destroyDelay = 0.2f;
    private float mainTimer = 0.0f;
     
    public BulletType bullet_type = BulletType.Normal;

    public enum BulletType{
        Normal,
        Pierce,
        Blood
    }

    public ShootType shoot_type = ShootType.Normal;

    public enum ShootType{
        Normal,
        Sin,
        Hooming
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        //init
        mainTimer = 0.0f;
        t_Col2d = null;
        thisTrans = GetComponent<Transform>();
    }

    void OnDisable()
    {
        ObjectPooler.ReturnToPool(gameObject);
        //CancelInvoke();    // Monobehaviour에 Invoke가 있다면
    }

    void OnTriggerEnter2D(Collider2D o)
    {
        t_Col2d = o;
    }

    // Update is called once per frame
    void Update()
    {
        mainTimer += Time.deltaTime;

        //Disable 조건
        if((t_Col2d != null)&&(t_Col2d.tag=="Obstacle"))
            gameObject.SetActive(false);
        else if(mainTimer >= destroyInterval)
            gameObject.SetActive(false);
        
        //움직임 처리
        thisTrans.Translate(Vector3.up * speed * Time.deltaTime);
        //사인파 움직임 처리
        thisTrans.rotation = Quaternion.Euler(new Vector3(0, 0, sin_size * Mathf.Sin(mainTimer*sin_rate+Mathf.PI/2)));

            //TODO:여기 사라지기전 모션 추가
            //StartCoroutine(destoryMotion());
    }

    IEnumerator destoryMotion()
    {
        //thisRbody2d.velocity = up;
        yield return new WaitForSeconds(destroyDelay);
        //---
        //gameObject.SetActive(false);
    }
}
