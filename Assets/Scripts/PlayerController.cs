using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed = 1.0f;
    public float runMultiplier = 2.0f;
    public float delay = 0.1f;
    public static GameObject instance = null;

    private Animator anim;
    private Rigidbody2D rb;

    private bool playerMoving;
    private Vector2 lastMove = Vector2.zero;
    private bool playerRunning;
    private float t = 0.0f;
    
	void Start () {
        if (instance == null)
        {
            instance = this.gameObject;
        }
        else if (instance != this.gameObject)
        {
            Destroy(this.gameObject);
        }

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        DontDestroyOnLoad(this.gameObject);
	}
	
	void Update () {
        playerMoving = false;
        playerRunning = false;

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        Vector3 playerVelocity = Vector3.zero;

        

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

        rb.velocity = playerVelocity;

        if (Time.time < t + delay && Time.time > delay)
        {
            anim.SetFloat("MoveX", lastMove.x);
            anim.SetFloat("MoveY", lastMove.y);
            anim.SetFloat("LastMoveX", lastMove.x);
            anim.SetFloat("LastMoveY", lastMove.y);
        }
        else
        {
            anim.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
            anim.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
        }

        
        anim.SetBool("PlayerMoving", playerMoving);
        anim.SetBool("PlayerRunning", playerRunning);
        anim.SetFloat("LastMoveX", lastMove.x);
        anim.SetFloat("LastMoveY", lastMove.y);
	}
}
