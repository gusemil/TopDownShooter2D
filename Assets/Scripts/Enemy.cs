using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp;
    public int damage;
    public float moveSpeed;
    public int enemyPointValue;

    private GameObject player;
    private Vector2 direction;
    private float enemyDeathVolume = 0.5f;
    private Rigidbody2D rb;
    private bool isEnemyDead;
    private PlayerStats playerStats;
    private EnemyWaves enemyWaves;
    private GameManager gm;
    private PickupSystem pickupSystem;

    public GameObject deathAnimation;
    public GameObject bloodParticleEffect;
    public GameObject bloodSplatter;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemyWaves = GameManager.instance.EnemyWaves;
        gm = GameManager.instance;
        rb = GetComponent<Rigidbody2D>();
        isEnemyDead = false;
        pickupSystem = GameManager.instance.PickupSystem;

        moveSpeed += (float)enemyWaves.Wave * 0.35f;
    }

    void FixedUpdate()
    {
        EnemyMovementTowardsPlayer();
    }

    public void TakeDamage(int dmg, bool isKilledByBomb)
    {
        hp -= dmg;

        if (hp <= 0 && !isEnemyDead)
        {
            if (!isKilledByBomb)
            {
                gm.AddPoints(enemyPointValue);
                pickupSystem.SpawnPickUpFromEnemy(this.gameObject);
            }

            AudioManager.instance.PlaySound(27, enemyDeathVolume);
            DeathEffect();
            if (LevelManager.instance.IsBloodOn)
            {
                BloodEffect();
            }
            enemyWaves.EnemiesAlive--;
            isEnemyDead = true;
            Destroy(gameObject);
        }
    }

    private void DeathEffect()
    {
        AudioManager.instance.PlaySound(27, enemyDeathVolume);
        GameObject effect = Instantiate(deathAnimation, transform.position, Quaternion.identity);
        Destroy(effect, 0.3f);
    }

    private void BloodEffect()
    {
        GameObject effect = Instantiate(deathAnimation, transform.position, Quaternion.identity);
        GameObject burst = Instantiate(bloodParticleEffect, transform.position, Quaternion.identity);
        GameObject splatter = Instantiate(bloodSplatter, transform.position, Quaternion.identity);
        Destroy(effect, 0.2f);
    }


    private void EnemyMovementTowardsPlayer()
    {
        direction = player.transform.position - transform.position; //direction vector to player position
        direction.Normalize(); //convert to unit vector
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }
}

