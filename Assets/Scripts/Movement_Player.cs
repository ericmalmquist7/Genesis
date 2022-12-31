using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_Player : MonoBehaviour
{
    
    [Range(0.01f, 0.5f)]
    public float speed;

    private SceneManagerSingleton SM;
    private DoorController sceneDoor;

    public void Start()
    {
        DontDestroyOnLoad(this);

        GameObject sceneGO = GameObject.FindGameObjectWithTag("SceneManagerSingleton");
        SM = sceneGO.GetComponent<SceneManagerSingleton>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if(sceneDoor != null)
            {
               SM.changeScene(sceneDoor.targetSceneName, sceneDoor.targetPosition, gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("door"))
        {
            DoorController door = collision.gameObject.GetComponent<DoorController>();
            if (door.changesScene && door.isOpen)
            {
                sceneDoor = door;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("door"))
        {
            if (collision.gameObject.GetComponent<DoorController>().changesScene)
            {
                sceneDoor = null;
            }
        }
    }

    void FixedUpdate()
    {
        Vector3 direction = Vector3.zero;
        
        if (Input.GetKey(KeyCode.W)) direction += Vector3.up;
        if (Input.GetKey(KeyCode.S)) direction += Vector3.down;
        if (Input.GetKey(KeyCode.A)) direction += Vector3.left;
        if (Input.GetKey(KeyCode.D)) direction += Vector3.right;
        
        transform.position += (direction.normalized * speed);
    }
}
