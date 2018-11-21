using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour {

    public float monsterSpeed = 1.0f;
    public float dodgeMultiplier = 0.1f;
    public float hitCoolDown = 1.0f;

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
            DoDamage dd = collision.gameObject.GetComponent<DoDamage>();
            if (dd != null && Time.time > time + hitCoolDown)
            {
                dd.Damage(1, (dd.gameObject.transform.position - transform.position).normalized);
                time = Time.time;
            }
        }
    }
}
