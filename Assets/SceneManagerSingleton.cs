using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerSingleton : MonoBehaviour
{
    public static SceneManagerSingleton instance { get; private set; }

    public GameObject playerPrefab;
    private PlayerData playerData;

    class PlayerData
    {
        public Vector3 position;
        public Quaternion rotation;
    }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    public void Start()
    {
        DontDestroyOnLoad(this);

        //Temporary
        playerData = new PlayerData
        {
            position = new Vector3(0, 0, 0),
            rotation = new Quaternion(0, 0, 0, 0)
        };

        loadPlayer();
    }

    private void loadPlayer()
    {
        Instantiate(playerPrefab, playerData.position, playerData.rotation, null);
    }

    public void changeScene(string targetSceneName, Vector3 newLocation, GameObject entity)
    {
        StartCoroutine(changeSceneAsync(targetSceneName, newLocation, entity));
    }

    IEnumerator changeSceneAsync(string targetSceneName, Vector3 newLocation, GameObject entity)
    {
        AsyncOperation loading = SceneManager.LoadSceneAsync(targetSceneName);
        while (!loading.isDone)
        {
            yield return null;
        }
        entity.transform.position = newLocation;
    }
}
