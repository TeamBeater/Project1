using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour {

    public float speed = 8.0f;
    public float disappearDelay = 1.0f;
    public int damage = 1;

    private Rigidbody2D rb;

	public void Fire (Vector3 direction)
    {
        rb = GetComponent<Rigidbody2D>();

        if (direction == Vector3.zero)
        {
            rb.velocity = new Vector3(0.0f, -speed, 0.0f);
        }
        else
        {
            rb.velocity = direction.normalized * speed;
        }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HealthController healthController = collision.gameObject.GetComponent<HealthController>();
        if (healthController != null)
        {
            healthController.Damage(damage);
            Destroy(this.gameObject);
        }
        else
        {
            rb.velocity = Vector3.zero;
            StartCoroutine("StuckAtWall");
        }
    }

    IEnumerator StuckAtWall()
    {
        yield return new WaitForSecondsRealtime(disappearDelay);
        Destroy(this.gameObject);
    }
}
