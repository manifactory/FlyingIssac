using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResolution : MonoBehaviour
{
    public float x_adjust;
    void Start()
    {
        InvokeRepeating("CameraUpdate", 0,0.05f);
    }

    void CameraUpdate()
    {
        Camera camera = GetComponent<Camera>();
        Rect rect = camera.rect;
        float scaleheight = ((float)(Screen.width) / Screen.height) / ((float)8 / 10); // (가로 / 세로)
        float scalewidth = 1f / scaleheight;
        if (scaleheight < 1)
        {
            rect.height = scaleheight;
            rect.y = (1f - scaleheight) / 2f;
        }
        else
        {
            rect.width = scalewidth;
            rect.x = (1f - scalewidth - (x_adjust * scalewidth) ) / 2f;
        }
        camera.rect = rect;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F11))
        {
            Screen.fullScreen = !Screen.fullScreen;
        }
    }

    void OnPreCull() => GL.Clear(true, true, Color.black);
}