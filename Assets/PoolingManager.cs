using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingManager : MonoBehaviour
{

    public static PoolingManager instance;

    private Hashtable ObjectPool = new Hashtable();



    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        ObjectPool.Clear();
        Debug.Log("pool compleate");
    }

    public void AddObjectPool(string name, uint size, GameObject o_object)
    {
        ObjectPool.Add(name, new Stack<GameObject>());
        for(int i=0;i<size;i++)
        {
            //프리팹 오브젝트 맵상 위치,각도 무시
            
            GameObject t_object = Instantiate(o_object, Vector3.zero, Quaternion.identity);
            Stack<GameObject> t_stack = ObjectPool[name] as Stack<GameObject>;
            t_stack.Push(t_object);
            t_object.SetActive(false);
        }
    }

    public void ReturnObject(string name, GameObject p_object)
    {
        Stack<GameObject> t_stack = ObjectPool[name] as Stack<GameObject>;
        t_stack.Push(p_object);
        p_object.SetActive(false);
    }

    public GameObject GetObject(string name)
    {
        Stack<GameObject> t_stack = ObjectPool[name] as Stack<GameObject>;
        GameObject t_object = t_stack.Pop();
        t_object.SetActive(true);
        return t_object;
    }
}
