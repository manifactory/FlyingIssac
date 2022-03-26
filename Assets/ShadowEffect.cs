using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ShadowEffect : MonoBehaviour
{
    private Vector3 s_position;
    public float height = 0.0f;

    public float s_height = 0.0f;
    public float m_height = 5.0f;

    private static float transitionSpeed = 1.5f;


    // Start is called before the first frame update
    void Reset()
    {
        s_position = this.transform.localPosition;
    }

    void Awake()
    {
        height = s_height;
        InvokeRepeating("UpdatePosition",0.01f,0.01f);
    }

    void OnDisable()
    {
        height = s_height;
        CancelInvoke();
    }

    void UpdatePosition()
    {
        this.transform.localPosition = new Vector3(0,-height,0)+s_position;
    }

    public void StartAnimation(string anmationName)//내가 생각해도 멍청한 함수다.
    {
        InvokeRepeating(anmationName,0.01f,0.01f);
    }

    void IncreaseAnimation()
    {
        //Debug.Log(height);
        if(height == m_height)
            CancelInvoke("IncreaseAnimation");
        else
            height = Mathf.Lerp(height,m_height,Time.fixedDeltaTime*transitionSpeed);
    }

    void DecreaseAnimation()
    {
        if(height == s_height)
            this.GetComponentInParent<GameObject>().SetActive(false);
        else
            height = Mathf.Lerp(height,s_height,Time.fixedDeltaTime*transitionSpeed);
    }
}
