using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    
    public float delay = 0.1f;
    public float attackCoolDown = 1.0f;
    public Text text;
    public Throwable throwable;
    
    private CharacterActions characterActions;
    private bool playerMoving;
    private bool playerRunning;
    private float lastAttack = 0.0f;
    private static GameObject instance = null;
    private ActiveSceneManager activeSceneManager;

    void Start () {
        characterActions = GetComponent<CharacterActions>();

        if (instance == null)
        {
            instance = this.gameObject;
        }
        else if (instance != this.gameObject)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

        activeSceneManager = GameObject.FindGameObjectWithTag("ActiveSceneManager").GetComponent<ActiveSceneManager>();
    }
	
	void Update () {

        characterActions.ChangeVelocity(new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0.0f), Input.GetAxisRaw("Run") != 0.0f);

        if (Input.GetAxisRaw("Menu") != 0.0f)
        {
            activeSceneManager.SceneChange("Menu", Vector3.zero, Vector3.zero, true);
        }

        if (Input.GetAxisRaw("Fire2") != 0.0f)
        {
            characterActions.Throw(throwable, transform.position, transform.rotation);
        }
	}

    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Time.time > lastAttack + attackCoolDown && Input.GetAxisRaw("Fire1") != 0.0f)
        {
            collision.gameObject.GetComponent<CharacterActions>().Damage(1, (collision.gameObject.transform.position - transform.position).normalized);
            lastAttack = Time.time;
        }
    }

    public void YouDied()
    {
        activeSceneManager.SceneChange("Menu", Vector3.zero, Vector3.zero, true);
    }
}
