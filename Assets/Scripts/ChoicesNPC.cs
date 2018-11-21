using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoicesNPC : MonoBehaviour
{
    private bool playerIsInTrigger = false;
    private bool askedQuestion = false;
    public GameObject UI;
    public UIController uiController;

    private void Start()
    {
        UI = GameObject.Find("Main UI");
        uiController = UI.GetComponent<UIController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIsInTrigger)
        {
            uiController.DisplayMessage("Choose an answer\n(1) answer 1\n(2) answer 2");
            askedQuestion = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && askedQuestion && playerIsInTrigger)
        {
            uiController.DisplayMessage("You answered 1");
            askedQuestion = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && askedQuestion && playerIsInTrigger)
        {
            uiController.DisplayMessage("You answered 2");
            askedQuestion = false;
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
