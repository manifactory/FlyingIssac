using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public string currentMap = "Basement";
    private Dictionary<string, Transform> TileMapList = new Dictionary<string, Transform>();

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(transform.childCount);
        for(int i=0; i<transform.childCount; i++)
        {
            Transform t_child = transform.GetChild(i);
            //Debug.Log(t_child.name);
            TileMapList.Add(t_child.name,t_child.transform);
            t_child.gameObject.SetActive(false);
        }
        SetMap(currentMap);
    }

    public void SetMap(string map_name)
    {
        TileMapList[currentMap].gameObject.SetActive(false);
        TileMapList[map_name].gameObject.SetActive(true);
        currentMap = map_name;
    }
}
