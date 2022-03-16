using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{

    private Rigidbody2D thisRbody2d = null;

    public float speed = 0;

    // Start is called before the first frame update
    void OnEnable()
    {
        thisRbody2d = GetComponent<Rigidbody2D>();
        thisRbody2d.AddForce(Vector2.up*speed);
        //StartCoroutine(DestroyBullet());
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(1.0f);
        PoolingManager.instance.InsertQueue(gameObject);
    }

    void OnTriggerEnter2D(Collider2D o)
    {
        PoolingManager.instance.InsertQueue(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
