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

    IEnumerator shotgun()
    {
        yield return new WaitForSeconds(0.5f);
        while(true)
        {
            float init_angle = mainTimer*50.0f;
            for(float i=-2; i<=2; i+=0.1f)
            {
                GameObject t_bullet = ObjectPooler.SpawnFromPool("bullet_enemy"
                ,gameObject.transform.position
                ,Quaternion.Euler(new Vector3(0,0,init_angle+180.0f*i)));
                bullet p_bullet = t_bullet.GetComponent<bullet>();
                p_bullet.Setup(new Vector3(0,0.5f,0),10.0f,"Player");
                p_bullet.SetSinValue(5.0f,5.0f);
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
