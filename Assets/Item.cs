using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int TakeScore = 5000;
    void OnDisable()
    {
        GameObject.Find("LevelManager").GetComponent<LevelManager>().mob_count_decrease();
        GameObject.Find("LevelManager").GetComponent<LevelManager>().AddScore(TakeScore);
    }
}
