using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    private static GameObject instance = null;

    public Text textThing;

    void Start () {
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

    public void DisplayMessage(string message, string text = "text")
    {
        switch (text)
        {
            default:
                textThing.text = message;
                break;
        }
    }
}
