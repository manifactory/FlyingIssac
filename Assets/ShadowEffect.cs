using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowEffect : MonoBehaviour
{
    private Vector3 s_position;
    // Start is called before the first frame update
    void Awake()
    {
        s_position = this.transform.localPosition;
        InvokeRepeating("UpdatePosition",0.01f,0.01f);
    }

    void OnDisable()
    {
        CancelInvoke();
    }

    void UpdatePosition()
    {
        this.transform.localPosition = new Vector3(0,-transform.GetComponentInParent<Transform>().position.z,0)+s_position;
    }
}
