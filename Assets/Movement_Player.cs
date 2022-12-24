using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_Player : MonoBehaviour
{
    
    [Range(0.01f, 0.5f)]
    public float speed;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        Vector3 direction = Vector3.zero;
        
        if (Input.GetKey(KeyCode.W)) direction = direction + Vector3.up;
        if (Input.GetKey(KeyCode.S)) direction = direction + Vector3.down;
        if (Input.GetKey(KeyCode.A)) direction = direction + Vector3.left;
        if (Input.GetKey(KeyCode.D)) direction = direction + Vector3.right;
        
        transform.position = transform.position + (direction.normalized * speed);
    }
}
