using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour {

    public int fullHealth = 10;
    public Vector2Int[] rewards = { new Vector2Int(100, 0) };
    public int health;

    private UIController uiController;
    private CharacterActions characterActions;

    void Start ()
    {
        uiController = GameObject.Find("Main UI").GetComponent<UIController>();
        characterActions = GetComponent<CharacterActions>();
        health = fullHealth;
        if (this.gameObject.tag == "Player" && uiController.healthText.text == "")
        {
            uiController.Health(health);
        }
    }
    
    public void Damage(int damage, Vector3 kick = new Vector3())
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (characterActions != null)
        {
            characterActions.OverrideMove(0.2f, kick);
        }

        health -= damage;

        if (this.gameObject == player)
        {
            uiController.Health(health);
        }

        if (health <= 0)
        {
            if (this.gameObject == player)
            {
                health = fullHealth;
                this.gameObject.GetComponent<PlayerController>().YouDied();
            }
            else
            {
                int prev = 0;
                int rand = Random.Range(0, 101);
                for (int i = 0; i < rewards.Length; i++)
                {
                    if (rand < prev + rewards[i].x && player != null)
                    {
                        player.GetComponent<MoneySystem>().Increase(rewards[i].y);
                        break;
                    }
                    prev += rewards[i].x;
                }
                Destroy(this.gameObject);
            }
        }
    }
}
