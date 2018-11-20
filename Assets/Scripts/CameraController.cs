using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 offset = new Vector3(0.0f, 0.0f, 1.0f);
    public GameObject parent;
    public static GameObject instance = null;

    void Start ()
    {
        if (instance == null)
        {
            instance = this.gameObject;
        }
        else if (instance != this.gameObject)
        {
            Destroy(this.gameObject);
        }

        transform.position = parent.transform.position + offset;
        DontDestroyOnLoad(this.gameObject);
	}
	
	void Update () {
        transform.position = parent.transform.position + offset;
	}
}
