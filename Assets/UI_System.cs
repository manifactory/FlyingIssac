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
