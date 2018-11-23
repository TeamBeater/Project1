using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkingNPC : MonoBehaviour {

    private bool playerIsInTrigger = false;
    private GameObject UI;
    private UIController uiController;

    private void Start()
    {
        UI = GameObject.Find("Main UI");
        uiController = UI.GetComponent<UIController>();
    }

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIsInTrigger)
        {
            uiController.DisplayMessage("Hello there!");
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerIsInTrigger = true;
            uiController.DisplayMessage("(E) to talk");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerIsInTrigger = false;
            uiController.DisplayMessage("");
        }
    }

    private void OnDestroy()
    {
        uiController.DisplayMessage("");
    }
}
