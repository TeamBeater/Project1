using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActiveSceneManager : MonoBehaviour
{
    public GameObject player;

    private string sceneToLoad;
    private Vector3 spawn = Vector3.zero;
    private Vector3 facing = Vector3.zero;
    
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
        player.transform.position = spawn;
        CharacterActions characterActions = player.GetComponent<CharacterActions>();
        characterActions.lastMove = facing;
        characterActions.anim.SetFloat("LastMoveX", facing.x);
        characterActions.anim.SetFloat("LastMoveY", facing.y);
    }

    public void SceneChange(string scene_temp, Vector3 spawn_temp, Vector3 facing_temp)
    {
        sceneToLoad = scene_temp;
        spawn = spawn_temp;
        facing = facing_temp;
        StartCoroutine("SceneChangeCoroutine");
    }

    IEnumerator SceneChangeCoroutine()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad);

        while (SceneManager.GetActiveScene().name != sceneToLoad)
        {
            yield return null;
        }

        /*
        characterActions.transform.position = spawn;
        characterActions.anim.SetFloat("LastMoveX", facing.x);
        characterActions.anim.SetFloat("LastMoveY", facing.y);
        */
    }
}
