using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
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
