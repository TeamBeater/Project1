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
    public Text text;
    
    private CharacterActions characterActions;
    private bool playerMoving;
    private bool playerRunning;
    private float textOnTime = 0.0f;
    private float lastAttack = 0.0f;
    private bool youDiedIsOn = false;
    private static GameObject instance = null;

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
    }
	
	void Update () {

        characterActions.ChangeVelocity(new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0.0f), Input.GetAxisRaw("Run") != 0.0f);

        if (youDiedIsOn && Time.time > textOnTime + 5.0f)
        {
            text.text = "";
            youDiedIsOn = false;
        }
	}

    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Time.time > lastAttack + attackCoolDown && Input.GetKeyDown(KeyCode.Mouse0))
        {
            collision.gameObject.GetComponent<CharacterActions>().Damage(1, (collision.gameObject.transform.position - transform.position).normalized);
            lastAttack = Time.time;
        }
    }

    IEnumerator YouDied()
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
