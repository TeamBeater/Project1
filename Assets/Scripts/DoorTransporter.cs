using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorTransporter : MonoBehaviour {

    public string sceneToLoad;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
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
        
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneToLoad));
    }
}
