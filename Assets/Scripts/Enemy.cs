using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp;
    public int damage;
    public float moveSpeed;
    public GameObject player;
    private Vector2 direction;
    private float timer = 0;
    private float damageCoolDown = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        direction = player.transform.position - transform.position; //direction vector to player position
        direction.Normalize(); //convert to unit vector

        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }

    void Update()
    {
        timer += Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (timer >= damageCoolDown)
        {
            DamagePlayerOnCollision(collision);
            timer = 0;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (timer >= damageCoolDown)
        {
            DamagePlayerOnCollision(collision);
            timer = 0;
        }
    }

    public void TakeDamage(int dmg)
    {
        hp -= dmg;
        Debug.Log("hp: " + gameObject.transform.tag + " " + hp);

        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void DamagePlayerOnCollision(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerStats playerStats = GameManager.status.PlayerStats;
            playerStats.TakeDamage(damage);
            if (playerStats.Hp <= 0)
            {
                Destroy(collision.gameObject); //player destruction might not be needed later
            }
        }
    }
}
