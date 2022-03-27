using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    private string[] Stage_order = new string[3]{"Basement","Cellar","Caves"};
    private List<GameObject> Mob_List = new List<GameObject>();
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

    void LevelLoad(int stage_num)
    {
        int mob_count = GameObject.Find("Level_"+stage_num).transform.childCount;
        for(int i=0;i<mob_count;i++)
        {
            
        }
        
    }
}
