using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_System : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetNamedTextElement(string parent_name, string ele_name, string _text)
    {
        GameObject.Find(parent_name).transform.Find(ele_name).GetComponent<Text>().text = _text;
    }
    
    public void SetNumberElement(string name, int value)
    {
        SetNamedTextElement(name, "number_element", ""+value);
    }

    public void SetIndicatorElement(string name, int value)
    {
        
    }
}
