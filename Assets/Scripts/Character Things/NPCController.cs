using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour {
    
    public List<Vector3> corners;

    private CharacterActions characterActions;
    private int nextCorner = 0;
    private bool playerIsInTrigger = false;

    void Start ()
    {
        characterActions = GetComponent<CharacterActions>();

        if (corners.Count > 0)
        {
            transform.position = corners[0];
        }
    }
	
	void Update ()
    {
        if (corners.Count > 1 && Mathf.Abs(corners[nextCorner].x - transform.position.x) < 0.01f && Mathf.Abs(corners[nextCorner].y - transform.position.y) < 0.01f)
        {
            nextCorner = (nextCorner + 1) % corners.Count;
        }

        if (corners.Count > 1 && !playerIsInTrigger)
        {
            characterActions.ChangeVelocity(corners[nextCorner] - transform.position, false);
        }
        else if (playerIsInTrigger)
        {
            characterActions.ChangeVelocity(Vector3.zero, false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerIsInTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerIsInTrigger = false;
        }
    }
}
