using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private Rect s_boundary;
    // Start is called before the first frame update
    void Start()
    {
        s_boundary.xMin = GameObject.Find("TopLeft").transform.position.x;
        s_boundary.xMax = GameObject.Find("BottomRight").transform.position.x;
        s_boundary.yMin = GameObject.Find("BottomRight").transform.position.y;
        s_boundary.yMax = GameObject.Find("TopLeft").transform.position.y;
    }

    public Rect GetStageBoundary()
    {
        return s_boundary;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            //맵 배경을 바꿉니다.
            GameObject.Find("MapGrid").GetComponent<MapManager>().SetMap("Basement");
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            GameObject.Find("MapGrid").GetComponent<MapManager>().SetMap("Caves");
        }
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            GameObject.Find("MapGrid").GetComponent<MapManager>().SetMap("Cellar");
        }
    }
}
