using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public Button play, quit;

    private void Start()
    {
        play.onClick.AddListener(Play);
        quit.onClick.AddListener(Quit);
    }

    void Play()
    {
        GameObject.FindGameObjectWithTag("ActiveSceneManager").GetComponent<ActiveSceneManager>().SceneChange("Main", Vector3.zero, Vector3.zero);
    }

    void Quit()
    {
        Application.Quit();
    }
}
