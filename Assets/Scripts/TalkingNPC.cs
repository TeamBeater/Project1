using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkingNPC : MonoBehaviour {

    public Text message;
    
    private bool playerIsInTrigger = false;
    

	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIsInTrigger)
        {
            message.text = "Hello there!";
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerIsInTrigger = true;
            message.text = "Press 'E'";
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
