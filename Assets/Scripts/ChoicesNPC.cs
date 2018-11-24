using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoicesNPC : MonoBehaviour
{
    private bool playerIsInTrigger = false;
    private bool askedQuestion = false;
    private GameObject UI;
    private UIController uiController;
    private int amtOfAmmoGiven = 10;

    private void Start()
    {
        UI = GameObject.Find("Main UI");
        uiController = UI.GetComponent<UIController>();
    }

    void Update()
    {
        if (Input.GetAxisRaw("Interact") != 0.0f && playerIsInTrigger)
        {
            uiController.DisplayMessage("Want more ammo?\n(1) Yes\n(2) No");
            askedQuestion = true;
        }

        if (Input.GetAxisRaw("Alpha1") != 0.0f && askedQuestion && playerIsInTrigger)
        {
            uiController.DisplayMessage("Here");
            GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterActions>().GetAmmo(amtOfAmmoGiven);
            askedQuestion = false;
        }
        else if (Input.GetAxisRaw("Alpha2") != 0.0f && askedQuestion && playerIsInTrigger)
        {
            uiController.DisplayMessage("What is wrong with you?");
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
