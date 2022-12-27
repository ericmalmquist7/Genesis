using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    
    public bool blocksMovement = true;

    public bool changesScene = false;
    public string targetSceneName;
    public Vector3 targetPosition;

    public bool isOpen = false;
    public Sprite spriteOpen;
    public Sprite spriteClosed;

    private SpriteRenderer sr;
    private BoxCollider2D bc;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();

        sr.sprite = (isOpen ? spriteOpen : spriteClosed);
    }

    void OnMouseDown()
    {
        changeState();
    }


    public void changeState(){
        isOpen = !isOpen;
        sr.sprite = (isOpen ? spriteOpen : spriteClosed);
        if (blocksMovement){
            bc.isTrigger = isOpen;
        }
        else{
            bc.isTrigger = true;
        }
    }
}
