using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoicesNPC : MonoBehaviour
{
    public Text message;

    private bool playerIsInTrigger = false;
    private bool askedQuestion = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIsInTrigger)
        {
            message.text = "Choose an answer\n(1) answer 1\n(2) answer 2";
            askedQuestion = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && askedQuestion && playerIsInTrigger)
        {
            message.text = "You answered 1";
            askedQuestion = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && askedQuestion && playerIsInTrigger)
        {
            message.text = "You answered 2";
            askedQuestion = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerIsInTrigger = true;
            message.text = "(E) to talk";
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerIsInTrigger = false;
            message.text = "";
        }
    }
}
