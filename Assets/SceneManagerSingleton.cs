using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class SceneManagerSingleton : MonoBehaviour
{
    public static SceneManagerSingleton instance { get; private set; }

    public GameObject playerPrefab;
    public PlayerData playerData;

    [Serializable]
    public struct PlayerData
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
