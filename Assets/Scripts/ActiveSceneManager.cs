using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActiveSceneManager : MonoBehaviour
{
    private static string sceneToLoad;
    private static Vector3 spawn = Vector3.zero;
    private static Vector3 facing = Vector3.zero;
    private static bool destroyPlayer = false;
    
    private static GameObject instance = null;

    private void Start()
    {
        if (instance == null)
        {
            instance = this.gameObject;
        }
        else if (instance != this.gameObject)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            if (destroyPlayer)
            {
                Destroy(player);
                Destroy(GameObject.FindGameObjectWithTag("MainCamera"));
                Destroy(GameObject.Find("Main UI"));
            }
            else
            {
                player.transform.position = spawn;
                CharacterActions characterActions = player.GetComponent<CharacterActions>();
                characterActions.lastMove = facing;
                characterActions.anim.SetFloat("LastMoveX", facing.x);
                characterActions.anim.SetFloat("LastMoveY", facing.y);
            }
        }
    }

    public void SceneChange(string scene_temp, Vector3 spawn_temp = new Vector3(), Vector3 facing_temp = new Vector3(), bool destroyPlayer_temp = false)
    {
        sceneToLoad = scene_temp;
        spawn = spawn_temp;
        facing = facing_temp;
        destroyPlayer = destroyPlayer_temp;
        StartCoroutine("SceneChangeCoroutine");
    }

    IEnumerator SceneChangeCoroutine()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
