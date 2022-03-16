using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootFunction : MonoBehaviour
{
    Transform thisTransform; 
    
    [Header("Shoot value")]

    [SerializeField]
    private GameObject bulletPrefab;
    Queue<GameObject> poolingObjectQueue = new Queue<GameObject>();

    public float shootInterval;

    public Vector2 spawnPos = Vector2.zero;
    public float spawnAngle = 0;

    // Start is called before the first frame update
    void Start()
    {
        thisTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Shoot()
    {
        poolingObjectQueue.Enqueue();
    }
}
