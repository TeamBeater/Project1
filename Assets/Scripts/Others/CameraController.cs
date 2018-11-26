using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 offset = new Vector3(0.0f, 0.0f, 1.0f);
    public GameObject parent;
    public bool lockPosition = false;

    private static GameObject instance = null;

    void Start()
    {
        transform.position = parent.transform.position + offset;

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
	
	void Update ()
    {
        if (!lockPosition)
        {
            transform.position = parent.transform.position + offset;
        }
	}
}
