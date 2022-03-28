using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

    private int Map_count = 0;

    private string[] Stage_order = new string[10];
    
    private int Level_count = 0;
    private int[] Level_entity_count = new int[100];
    private int current_level = 0;

    private int Score = 0;
    private int HighScore = 0;

    private Rect s_boundary;

    private int[] s_Level_entity_count = new int[100];
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
            Level_entity_count[i] = GameObject.Find("Levels").transform.GetChild(i).transform.childCount;
        }
        Debug.Log(Level_entity_count);
        Array.Copy(Level_entity_count,s_Level_entity_count, 100);
        

        InvokeRepeating("LevelManagerUpdate",0.01f,0.01f);
    }

    void LevelManagerUpdate()
    {
        if(Level_entity_count[current_level] == 1)
        {
            if(s_Level_entity_count[current_level]-1 != 0)
                GameObject.Find("Levels")
                    .transform.GetChild(current_level)
                    .transform.GetChild(s_Level_entity_count[current_level]-1)
                    .gameObject.SetActive(true);
        }
        if(Level_entity_count[current_level] <= 0)
        {
            LoadNextStage();
        }
    }

    public void AddScore(int s)
    {
        Score += s;
        Score = Score <= 0 ? 0 : Score;
        GameObject.Find("Canvas").GetComponent<UI_System>().SetNumberElement("Score", Score);
        if(HighScore<Score)
        {
            HighScore = Score;
            GameObject.Find("Canvas").GetComponent<UI_System>().SetNumberElement("HiScore", HighScore);
        }
    }

    void OnDisable()
    {
        CancelInvoke();
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
                GameObject.Find("Canvas").transform.Find("LevelClear").GetComponent<Text>().color = new Color(1,1,1,0.5f);
                CancelInvoke();
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
                GameObject.Find("Canvas").transform.Find("LevelClear").GetComponent<Text>().color = new Color(1,1,1,0.5f);
                CancelInvoke();
                return;
            }
            else
            {
                SetLevelActive(current_level, true);
                //아이템 엔티티 비활성화
                if(s_Level_entity_count[current_level]-1 != 0)
                {
                    GameObject.Find("Levels")
                        .transform.GetChild(current_level)
                        .transform.GetChild(s_Level_entity_count[current_level]-1)
                        .gameObject.SetActive(false);
                    //아이템 비활성화시 카운트 감소로 인한 보정
                    Level_entity_count[current_level]++;
                }
                //플레이어 위치 설정
                GameObject.Find("Player").GetComponent<player>().targetPos = new Vector3(0,-3.15f,0);

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
        
        for(int i=0;i<Level_entity_count[lv_num];i++)
        {
            GameObject.Find("Levels")
                .transform.GetChild(lv_num)
                .transform.GetChild(i)
                .gameObject.SetActive(active);
        }
    }

    public bool mob_count_decrease()
    {
        Level_entity_count[current_level]--;
        Debug.Log(Level_entity_count[current_level]);
        return Level_entity_count[current_level] <= 0;
    }

}
