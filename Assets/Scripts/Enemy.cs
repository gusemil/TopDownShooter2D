using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp;
    public int damage;
    public float moveSpeed;
    private GameObject player;
    private Vector2 direction;
    private float enemyDamageTimer = 0;
    private float enemyDamageCooldown = 0.5f;

    PlayerStats playerStats;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerStats = GameManager.status.PlayerStats;
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
        enemyDamageTimer += Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if ( (enemyDamageTimer >= enemyDamageCooldown) && other.gameObject.tag == "Player")
        {
            DamagePlayerOnCollision(other);
            enemyDamageTimer = 0;
        }
        /*else if (playerStats.IsDashing && other.gameObject.tag == "Player")
        {
            Physics.IgnoreCollision(player.GetComponent<Collider>(),this.GetComponent<Collider>());
        }*/
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if ( (enemyDamageTimer >= enemyDamageCooldown) && other.gameObject.tag == "Player")
        {
            DamagePlayerOnCollision(other);
            enemyDamageTimer = 0;
        }
        /*
        else if (playerStats.IsDashing && other.gameObject.tag == "Player")
        {
            Physics.IgnoreCollision(player.GetComponent<Collider>(), this.GetComponent<Collider>());
        }
        */
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

    public void DamagePlayerOnCollision(Collision2D other)
    {
            PlayerStats playerStats = GameManager.status.PlayerStats;
            playerStats.TakeDamage(damage);
            if (playerStats.Hp <= 0)
            {
                Destroy(other.gameObject); //player destruction might not be needed later
            }
    }
}
