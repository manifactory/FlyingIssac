using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    private int Map_count = 0;

    
    private int Level_count = 0;
    private int[] Level_mob_count = new int[100];
    private int current_level = 1;

    private Rect s_boundary;
    // Start is called before the first frame update
    void Start()
    {
        s_boundary.xMin = GameObject.Find("TopLeft").transform.position.x;
        s_boundary.xMax = GameObject.Find("BottomRight").transform.position.x;
        s_boundary.yMin = GameObject.Find("BottomRight").transform.position.y;
        s_boundary.yMax = GameObject.Find("TopLeft").transform.position.y;

        Map_count = GameObject.Find("MapGrid").transform.childCount;

        Level_count = GameObject.Find("Levels").transform.childCount;
        Level_mob_count[0] = 0;
        for(int i=0;i<Level_count;i++)
        {
            Level_mob_count[i+1] = GameObject.Find("Levels").transform.GetChild(i).transform.childCount;
        }
        Debug.Log(Level_mob_count);

        InvokeRepeating("LevelManagerUpdate",0.01f,0.01f);
    }

    void LevelManagerUpdate()
    {
        if(Level_mob_count[current_level] <= 0)
        {
            LoadNextStage();
        }
    }

    public Rect GetStageBoundary()
    {
        return s_boundary;
    }

    void LevelLoad(int lv_num)
    {
        lv_num--;

        GameObject.Find("Levels")
            .transform.GetChild(lv_num)
            .gameObject.SetActive(true);

        for(int i=0;i<Level_mob_count[lv_num];i++)
        {
            GameObject.Find("Levels")
                .transform.GetChild(lv_num)
                .transform.GetChild(i)
                .gameObject.SetActive(true);
        }
    }

    public void LoadNextStage()
    {
        GameObject.Find("Levels")
            .transform.GetChild(current_level)
            .gameObject.SetActive(false);

        GameObject.Find("MapGrid")
            .transform.GetChild(current_level)
            .gameObject.SetActive(false);
        
        current_level++;
        Debug.Log("currnet_level "+current_level);
        LevelLoad(current_level);

        GameObject.Find("MapGrid")
            .transform.GetChild(current_level)
            .gameObject.SetActive(false);
    }

    public bool mob_count_decrease()
    {
        Level_mob_count[current_level]--;
        Debug.Log(Level_mob_count[current_level]);
        return Level_mob_count[current_level-1] <= 0;
    }

}
