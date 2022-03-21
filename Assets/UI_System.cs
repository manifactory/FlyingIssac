using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_System : MonoBehaviour
{

    public static UI_System instance = null;

    // Start is called before the first frame update
    void Start()
    {
        //인디케이터 쓰려면 초기화 해야함... 왜 이렇게 됐을까... 나중에 고치자
        init_indicator("Bomb", 5, 40);
    }

    void init_indicator(string name, uint icon_count_max, int icon_pos_gap)
    {
        GameObject origin_icon = GameObject.Find(name).transform.Find("indicator_element").gameObject;
    }

    void SetNamedTextElement(string parent_name, string ele_name, string _text)
    {
        GameObject.Find(parent_name).transform.Find(ele_name).GetComponent<Text>().text = _text;
    }
    
    void SetNumberElement(string name, int value)
    {
        SetNamedTextElement(name, "number_element", ""+value);
    }

    void SetIndicatorElement(string name, int value)
    {
        
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
