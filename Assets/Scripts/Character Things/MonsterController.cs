﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour {
    
    public float hitCoolDown = 1.0f;
    public int reward = 10;

    private GameObject player;
    private CharacterActions characterActions;
    private float time;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        characterActions = GetComponent<CharacterActions>();
    }

    void Update () {
        characterActions.ChangeVelocity(player.transform.position - transform.position, false);
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && collision is BoxCollider2D)
        {
            if (Time.time > time + hitCoolDown)
            {
                collision.GetComponent<HealthController>().Damage(1, (collision.gameObject.transform.position - transform.position).normalized);
                time = Time.time;
            }
        }
    }
}
