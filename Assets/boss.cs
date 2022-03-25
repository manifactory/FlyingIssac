using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss : MonoBehaviour
{
    float mainTimer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("shotgun",0.0f,0.5f);
        StartCoroutine(shotgun());
    }

    void OnTriggerEnter2D(Collider2D o)
    {
        //GetComponent는 클래스내 변수에 할당해서 자원 소비를 줄여야 되는데 편의상 패스
        if(o.GetComponent<bullet>().target == "Enemy")
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            o.gameObject.SetActive(false);
        }
    }

    void OnTriggerExit2D(Collider2D o)
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    IEnumerator shotgun()
    {
        yield return new WaitForSeconds(0.5f);
        while(true)
        {
            float init_angle = mainTimer*50.0f;
            for(float i=-2; i<=2; i+=0.1f)
            {
                GameObject t_bullet = ObjectPooler.SpawnFromPool("bullet_enemy"
                ,this.transform.position
                ,Quaternion.Euler(new Vector3(0,0,init_angle+180.0f*i)));
                bullet p_bullet = t_bullet.GetComponent<bullet>();
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        mainTimer+=Time.deltaTime;
    }
}
