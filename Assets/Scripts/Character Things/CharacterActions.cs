using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterActions : MonoBehaviour {

    public float speed = 1.0f;
    public float run = 2.0f;
    public float kickMultiplier = 1.0f;
    public float throwCoolDown = 0.5f;
    public int maxAmmo = 20;

    [HideInInspector]
    public Animator anim = null;
    [HideInInspector]
    public Vector3 lastMove;
    private Rigidbody2D rb;
    private bool lockChangeVelocity = false;
    private UIController uiController;
    private float lastThrowTime = -1.0f;
    private int amtOfThrowables = 10;
    

	void Start ()
    {
        uiController = GameObject.Find("Main UI").GetComponent<UIController>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        if (this.gameObject.tag == "Player" && uiController.ammoText.text == "")
        {
            uiController.Ammo(amtOfThrowables);
        }
    }

    public void ChangeVelocity(Vector3 velocity, bool running)
    {
        if (!lockChangeVelocity)
        {
            bool moving = false;
            if (rb != null)
            {
                velocity = velocity.normalized * speed;
                if (running)
                    velocity *= run;
                rb.velocity = velocity;
            }
            else
            {
                transform.Translate(velocity.normalized * speed * Time.deltaTime);
            }

            if (velocity != Vector3.zero)
            {
                moving = true;
                lastMove = velocity;
            }

            if (anim != null)
            {
                anim.SetBool("isMoving", moving);
                anim.SetBool("isRunning", running);
                anim.SetFloat("MoveX", velocity.x);
                anim.SetFloat("MoveY", velocity.y);
                anim.SetFloat("LastMoveX", lastMove.x);
                anim.SetFloat("LastMoveY", lastMove.y);
            }
        }
    }

    public void OverrideMove(float duration, Vector3 direction)
    {
        
        lockChangeVelocity = true;
        if (rb != null)
        {
            rb.velocity = direction * kickMultiplier;
        }
        StartCoroutine(OverrideOn(duration));
    }

    IEnumerator OverrideOn(float duration)
    {
        float time = Time.time;
        while (Time.time < time + duration)
        {
            yield return null;
        }
        lockChangeVelocity = false;
    }

    public void Throw(Throwable throwable, Vector3 position, Quaternion rotation)
    {
        if (amtOfThrowables > 0 && Time.time > lastThrowTime + throwCoolDown)
        {
            lastThrowTime = Time.time;
            amtOfThrowables--;
            uiController.Ammo(amtOfThrowables);
            Throwable throwableClone = (Throwable)Instantiate(throwable, position, rotation);
            throwableClone.Fire(lastMove);
        }
    }

    public int GetAmmo(int amt)
    {
        int ammoTaken = amt;
        amtOfThrowables += amt;
        if (amtOfThrowables > maxAmmo)
        {
            ammoTaken -= amtOfThrowables - maxAmmo;
            amtOfThrowables = maxAmmo;
        }
        uiController.Ammo(amtOfThrowables);
        return ammoTaken;
    }
}
