using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour {

    public float moveSpeed = 1.0f;
    public float diff = 0.01f;
    public List<Vector3> corners;

    private int nextCorner = 0;
    private bool playerIsInTrigger = false;

    void Start ()
    {
        if (corners.Count > 0)
        {
            transform.position = corners[0];
        }
    }
	
	void Update ()
    {
        if (corners.Count > 1 && Mathf.Abs(corners[nextCorner].x - transform.position.x) < diff && Mathf.Abs(corners[nextCorner].y - transform.position.y) < diff)
        {
            nextCorner = (nextCorner + 1) % corners.Count;
        }

        if (corners.Count > 1 && !playerIsInTrigger)
        {
            Vector3 npcVelocity = (corners[nextCorner] - transform.position).normalized * moveSpeed * Time.deltaTime;
            transform.Translate(npcVelocity);
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
