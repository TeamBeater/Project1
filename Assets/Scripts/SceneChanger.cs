using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string sceneToLoad = "Main";
    public Vector3 spawn;
    public Vector2 facing;

    private ActiveSceneManager activeSceneManager;

    private void Start()
    {
        activeSceneManager = GameObject.FindGameObjectWithTag("ActiveSceneManager").GetComponent<ActiveSceneManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && collision is BoxCollider2D)
        {
            activeSceneManager.SceneChange(sceneToLoad, spawn, facing);
        }
    }
}
