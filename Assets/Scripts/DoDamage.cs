using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DoDamage : MonoBehaviour {
    public float kickMultiplier = 1.0f;
    public int fullHealth = 10;
    public int health;

    private Rigidbody2D rb;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        health = fullHealth;
    }

    public void Damage(int damage, Vector3 kick = new Vector3())
    {
        if(rb != null)
            rb.AddForce(kick * kickMultiplier, ForceMode2D.Impulse);

        health -= damage;
        if (health <= 0)
        {
            if (this.gameObject.tag == "Player")
            {
                health = fullHealth;
                this.gameObject.GetComponent<PlayerController>().StartCoroutine("YouDied");
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
}
