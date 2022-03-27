using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss : MonoBehaviour
{
    float mainTimer = 0.0f;

    // Start is called before the first frame update
    void OnEnable()
    {
        InvokeRepeating("shotgun",0.0f,0.5f);
    }

    void OnDisable()
    {
        CancelInvoke();
    }

    void OnTriggerEnter2D(Collider2D o)
    {
        if(o.GetComponent<bullet>().target == "Enemy")
        {
            Invoke("GetDamage",0);
            o.gameObject.SetActive(false);
        }
    }

    void OnTriggerExit2D(Collider2D o)
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    void shotgun()
    {
        float init_angle = mainTimer*50.0f;
        for(float i=-1; i<=1; i+=0.1f)
        {
            GameObject.Find("ShootBulletWraper").GetComponent<ShootBulletWraper>()
            .ShootBullet("bullet_enemy", this.transform.position, degree: init_angle+180.0f*i);
        }
    }

    void GetDamage()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        Invoke("ColorReset",0.3f);
    }

    void ColorReset()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        mainTimer+=Time.deltaTime;
    }
}
