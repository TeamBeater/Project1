using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTransporter : MonoBehaviour {

    public string sceneToLoad;
    public GameObject mainCamera;

    private GameObject player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = collision.gameObject;
            StartCoroutine("LoadScene");
        }
    }

    IEnumerator LoadScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad);
        asyncLoad.allowSceneActivation = true;

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        //SceneManager.MoveGameObjectToScene(player, SceneManager.GetSceneByName(sceneToLoad));
        //SceneManager.MoveGameObjectToScene(mainCamera, SceneManager.GetSceneByName(sceneToLoad));
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneToLoad));
    }
}
