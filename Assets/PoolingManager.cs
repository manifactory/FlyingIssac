using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingManager : MonoBehaviour
{

    public static PoolingManager instance;

    public GameObject origin_prefab = null;
    public uint POOLING_LIMIT = 100;

    private Queue<GameObject> m_queue = new Queue<GameObject>();



    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        m_queue.Clear();
        for(int i=0;i<POOLING_LIMIT;i++)
        {
            //프리팹 오브젝트 맵상 위치,각도 무시
            GameObject t_object = Instantiate(origin_prefab, Vector3.zero, Quaternion.identity);
            m_queue.Enqueue(t_object);
            t_object.SetActive(false);
        }
        Debug.Log("pool compleate");
    }

    public void InsertQueue(GameObject p_object)
    {
        m_queue.Enqueue(p_object);
        p_object.SetActive(false);
    }

    public GameObject GetQueue()
    {
        GameObject t_object = m_queue.Dequeue();
        t_object.SetActive(true);
        return t_object;
    }
}
