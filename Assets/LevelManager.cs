using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    private int Map_count = 0;

    private string[] Stage_order = new string[10];
    
    private int Level_count = 0;
    private int[] Level_mob_count = new int[100];
    private int current_level = 0;

    private Rect s_boundary;
    // Start is called before the first frame update
    void Start()
    {
        s_boundary.xMin = GameObject.Find("TopLeft").transform.position.x;
        s_boundary.xMax = GameObject.Find("BottomRight").transform.position.x;
        s_boundary.yMin = GameObject.Find("BottomRight").transform.position.y;
        s_boundary.yMax = GameObject.Find("TopLeft").transform.position.y;

        Map_count = GameObject.Find("MapGrid").transform.childCount;
        for(int i=0;i<Map_count;i++)
        {
            Stage_order[i] = GameObject.Find("MapGrid").transform.GetChild(i).name;
        }
        Debug.Log(Stage_order);

        Level_count = GameObject.Find("Levels").transform.childCount;
        for(int i=0;i<Level_count;i++)
        {
            Level_mob_count[i] = GameObject.Find("Levels").transform.GetChild(i).transform.childCount;
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

    public void LoadNextStage()
    {
        if(!(current_level < 0))
        {
            if(current_level >= Level_count)
            {
                Debug.Log("LevelClear");
                return;
            }
            else
            {
                SetLevelActive(current_level, false);
                List<GameObject> bulletList = ObjectPooler.GetAllPools("bullet_player");
                foreach(GameObject b in bulletList)
                {
                    b.SetActive(false);
                }
                bulletList = ObjectPooler.GetAllPools("bullet_enemy");
                foreach(GameObject b in bulletList)
                {
                    b.SetActive(false);
                }
            }
            
            current_level++;
            Debug.Log("currnet_level "+current_level);

            if((current_level >= Level_count))
            {
                Debug.Log("LevelClear");
                return;
            }
            else
            {
                SetLevelActive(current_level, true);

                GameObject.Find("MapGrid")
                    .GetComponent<MapManager>().SetMap(Stage_order[current_level]);
            }
        }
    }

    void SetLevelActive(int lv_num, bool active)
    {
        GameObject.Find("Levels")
            .transform.GetChild(lv_num)
            .gameObject.SetActive(active);
        
        for(int i=0;i<Level_mob_count[lv_num];i++)
        {
            GameObject.Find("Levels")
                .transform.GetChild(lv_num)
                .transform.GetChild(i)
                .gameObject.SetActive(active);
        }
    }

    public bool mob_count_decrease()
    {
        Level_mob_count[current_level]--;
        Debug.Log(Level_mob_count[current_level]);
        return Level_mob_count[current_level] <= 0;
    }

}
