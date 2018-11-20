using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour {

    public float monsterSpeed = 1.0f;
    public float dodgeMultiplier = 0.1f;
    public float hitCoolDown = 1.0f;
    public int health = 5;

    private GameObject player;
    private GameObject[] monsterList;
    private Rigidbody2D rb;
    private float time;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        monsterList = GameObject.FindGameObjectsWithTag("Monster");
    }

    void Update () {
        Vector3 monsterVelocity = (player.transform.position - transform.position).normalized * monsterSpeed;
        Vector3 monsterSeparation;
        foreach(GameObject monster in monsterList)
        {
            if (monster != this.gameObject)
            {
                monsterSeparation = transform.position - monster.transform.position;
                monsterVelocity += monsterSeparation.normalized * dodgeMultiplier * (1.0f / Mathf.Max(1.0f, monsterSeparation.magnitude));
            }
            
        }
        rb.velocity = monsterVelocity;
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerController pc = collision.gameObject.GetComponent<PlayerController>();
            if (pc != null && Time.time > time + hitCoolDown)
            {
                pc.TakeDamage(1, (player.transform.position - transform.position).normalized);
                time = Time.time;
            }
        }
    }

    public void TakeDamage(int damage, Vector3 kick)
    {
        rb.AddForce(kick);
        health -= damage;
        Debug.Log(health);
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
