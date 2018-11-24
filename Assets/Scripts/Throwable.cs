﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour {

    public float speed = 6.0f;

	public void Fire (Vector3 direction) {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction.normalized * speed;
	}
}
