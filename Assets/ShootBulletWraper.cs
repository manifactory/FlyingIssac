using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBulletWraper : MonoBehaviour
{
    public void ShootBullet(string tag, Vector3 pos, Vector3? velo = null, Vector3? addvelo = null, float degree = 0.0f)
    {
        GameObject t_bullet = ObjectPooler.SpawnFromPool(tag,pos);
        bullet p_bullet = t_bullet.GetComponent<bullet>();
        if(velo != null)
        {
            velo = Quaternion.Euler(0, 0, degree) * velo;
            p_bullet.velocity = (Vector3)velo;
        }
        else if(addvelo != null)
        {
            addvelo += p_bullet.getStaticVelocity();
            addvelo = Quaternion.Euler(0, 0, degree) * addvelo;
            p_bullet.velocity = (Vector3)addvelo;
        }
        else
        {
            p_bullet.velocity = Quaternion.Euler(0, 0, degree) * p_bullet.velocity;
        }
    }
}
