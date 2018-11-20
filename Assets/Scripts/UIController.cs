using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {

    private static GameObject instance = null;

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
}
