using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    private string[] Map_order;




    private Rect s_boundary;
    // Start is called before the first frame update
    void Start()
    {
        s_boundary.xMin = GameObject.Find("TopLeft").transform.position.x;
        s_boundary.xMax = GameObject.Find("BottomRight").transform.position.x;
        s_boundary.yMin = GameObject.Find("BottomRight").transform.position.y;
        s_boundary.yMax = GameObject.Find("TopLeft").transform.position.y;
        int map_count = GameObject.Find("MapGrid").transform.childCount;
        for(int i=0;i<map_count;i++)
        {
            Map_order[i] = GameObject.Find("MapGrid").transform.GetChild(i).name;
        }
        Debug.Log(Map_order);
    }

    public Rect GetStageBoundary()
    {
        return s_boundary;
    }

    void LevelLoad(int stage_num)
    {
        int mob_count = GameObject.Find("Level_"+stage_num).transform.childCount;
        for(int i=0;i<mob_count;i++)
        {

        }
        
    }
}
