using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{

    private Transform t_transform = null;
    private Collider2D t_col2d = null;

    public float speed = 0;
    public float destroyTime = 1.0f;
    private float mainTimer = 0.0f;


    public float sin_rate = 0.0f;
    public float sin_size = 0.0f;
     
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
        t_col2d = null;
        t_transform = GetComponent<Transform>();

        Invoke(nameof(DeactiveDelay), destroyTime);
    }

    void DeactiveDelay() => gameObject.SetActive(false);

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
        if((t_col2d != null)&&(t_col2d.tag=="Obstacle"))
            gameObject.SetActive(false);
        
        //움직임 처리
        t_transform.Translate(Vector3.up * speed * Time.deltaTime);
        //사인파 움직임 처리
        t_transform.rotation = Quaternion.Euler(new Vector3(0, 0, sin_size * Mathf.Sin(mainTimer*sin_rate+Mathf.PI/2)));
    }
}
