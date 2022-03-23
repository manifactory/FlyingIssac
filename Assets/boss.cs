using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("pattern_example",0.0f,0.2f);
    }

    void pattern_example()
    {

        Debug.Log("test");
        for(float i=-2; i<=2; i+=0.05f)
        {
            GameObject t_bullet = ObjectPooler.SpawnFromPool("bullet"
            ,gameObject.transform.position
            ,Quaternion.Euler(new Vector3(0,0,180.0f*i)));
            bullet p_bullet = t_bullet.GetComponent<bullet>();
            p_bullet.Setup(1.0f,10.0f,"Player");
            p_bullet.SetSinValue(5.0f,5.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
