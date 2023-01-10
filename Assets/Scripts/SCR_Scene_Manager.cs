using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class SCR_Scene_Manager : MonoBehaviour
{
    public static SCR_Scene_Manager instance { get; private set; }
    public static GameObject playerReference { get; private set; }

    public GameObject playerPrefab;
    public PlayerData playerData;

    [Serializable]
    public struct PlayerData
    {
        public Vector3 position;
        public Quaternion rotation;
        public float health;
        public float healthMax;
        public float pain;
        public float painMax;
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
        playerReference = Instantiate(playerPrefab, playerData.position, playerData.rotation, null);
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
