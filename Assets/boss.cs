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

    IEnumerator shotgun()
    {
        yield return new WaitForSeconds(0.5f);
        while(true)
        {
            float init_angle = mainTimer*50.0f;
            for(float i=-2; i<=2; i+=0.1f)
            {
                GameObject.Find("ShootBulletWraper").GetComponent<ShootBulletWraper>()
                .ShootBullet("bullet_enemy", this.transform.position, degree: init_angle+180.0f*i);
            }
            yield return new WaitForSeconds(0.5f);
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
