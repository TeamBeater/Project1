using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float moveSpeed = 1.0f;
    public float runMultiplier = 2.0f;
    public float delay = 0.1f;
    public float attackCoolDown = 1.0f;
    public int health = 10;
    public Text text;
    
    private Animator anim;
    private Rigidbody2D rb;
    private bool playerMoving;
    private Vector2 lastMove = Vector2.zero;
    private bool playerRunning;
    private float t = 0.0f;
    private float textOnTime = 0.0f;
    private float lastAttack = 0.0f;
    private bool youDiedIsOn = false;
    private static GameObject instance = null;

    void Start () {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

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

        if (youDiedIsOn && Time.time > textOnTime + 5.0f)
        {
            text.text = "";
        }
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Monster" && Input.GetKeyDown(KeyCode.Mouse0) && Time.time > lastAttack + attackCoolDown)
        {
            MonsterController mc = collision.GetComponent<MonsterController>();
            mc.TakeDamage(1, (collision.gameObject.transform.position - transform.position).normalized);
            lastAttack = Time.time;
        }
    }

    public void TakeDamage(int damage, Vector3 kick)
    {
        rb.AddForce(kick);
        health -= damage;
        if (health <= 0)
        {
            health = 10;
            StartCoroutine("LoadScene");
        }
    }

    IEnumerator LoadScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Home");
        asyncLoad.allowSceneActivation = true;

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        transform.position = new Vector3(-4.5f, -5.5f, 0.0f);
        text.text = "You died";
        textOnTime = Time.time;
        youDiedIsOn = true;
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Home"));
    }
}
