using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed = 1;
    public float runMultiplier = 2;
    public float delay = 0.1f;

    private Animator anim;
    private Rigidbody2D rb;

    private bool playerMoving;
    private Vector2 lastMove;
    private bool playerRunning;
    private float t = 0.0f;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        playerMoving = false;
        playerRunning = false;

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        Vector3 playerVelocity = Vector3.zero;

        if (Time.time < t + delay && Time.time > delay)
        {
            playerVelocity = new Vector3(lastMove.x, lastMove.y, 0.0f);
            playerMoving = true;
            anim.SetFloat("MoveX", lastMove.x);
            anim.SetFloat("MoveY", lastMove.y);
        }
        else
        {
            if (moveX != 0.0f || moveY != 0.0f)
            {
                playerVelocity = new Vector3(moveX, moveY, 0.0f);
                playerMoving = true;
                lastMove = new Vector2(moveX, moveY);
            }

            playerVelocity *= moveSpeed;

            if (Input.GetAxisRaw("Run") != 0.0f)
            {
                playerRunning = true;
                playerVelocity *= runMultiplier;
            }

            if (moveX != 0 && moveY != 0)
            {
                playerVelocity /= 1.4142f;
                t = Time.time;
            }

            anim.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
            anim.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
        }

        if (playerVelocity != Vector3.zero)
            lastMove = (Vector2) playerVelocity;

        rb.velocity = playerVelocity;

        anim.SetBool("PlayerMoving", playerMoving);
        anim.SetBool("PlayerRunning", playerRunning);
        anim.SetFloat("LastMoveX", lastMove.x);
        anim.SetFloat("LastMoveY", lastMove.y);
	}
}
