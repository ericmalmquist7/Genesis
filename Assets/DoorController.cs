using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    
    public bool blocksMovement = true;

    public bool changesScene = false;
    public Scene targetScene;

    private bool isOpen = false;
    public Sprite open;
    public Sprite closed;

    private SpriteRenderer sr;
    private BoxCollider2D bc;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();
        Debug.Log(bc);
        sr.sprite = (isOpen ? open : closed);
    }

    void OnMouseDown()
    {
        changeState();
    }

    void OnTriggerStay2D(Collider2D other) {
        if(changesScene && targetScene != null){
            if(other.tag == "Player"){
                Debug.Log("Scene loading: " + targetScene);
                //SceneManager.LoadScene(targetScene, LoadSceneMode.Additive);
                //SceneManager.MoveGameObjectToScene(other.gameObject, targetScene);
            }
        }
    }

    public void changeState(){
        isOpen = !isOpen;
        sr.sprite = (isOpen ? open : closed);
        if (blocksMovement){
            bc.isTrigger = isOpen;
        }
        else{
            bc.isTrigger = true;
        }
    }
}
