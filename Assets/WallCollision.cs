using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollision : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D o)
    {
        if(o.tag == "Bullet")
            o.gameObject.SetActive(false);
    }
}
