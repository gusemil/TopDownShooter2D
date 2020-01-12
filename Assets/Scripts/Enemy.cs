using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp;
    public int damage;
    public float moveSpeed;
    public int enemyPointValue;
    public bool shooterEnemy;
    private GameObject player;
    private Vector2 direction;
    private float enemyDamageTimer = 0;
    private float enemyDamageCooldown = 0.5f;
    private float enemyDeathVolume = 0.5f;
    private Rigidbody2D rb2D;

    private float stopDistance = 7.5f;
    private float retreatDistance = 5f;
    private float playerEnemyDistance;

    public bool isEnemyDead;

    private PlayerStats playerStats;
    private EnemyWaves enemyWaves;
    private GameManager gm;
    private PickupSystem ps;

    public GameObject deathAnimation;
    public GameObject bloodParticleEffect;
    public GameObject bloodSplatter;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerStats = GameManager.instance.PlayerStats;
        enemyWaves = GameManager.instance.EnemyWaves;
        gm = GameManager.instance;
        rb2D = GetComponent<Rigidbody2D>();
        isEnemyDead = false;
        ps = GameManager.instance.PickupSystem;

        moveSpeed += (float)enemyWaves.Wave * 0.35f;
    }

    void FixedUpdate()
    {
        EnemyMovementTowardsPlayer();
    }

    // Update is called once per frame
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

    /*
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
    //}

    public void TakeDamage(int dmg, bool isKilledByBomb)
    {
        hp -= dmg;

        if (hp <= 0 && !isEnemyDead)
        {
            if (!isKilledByBomb)
            {
                gm.AddPoints(enemyPointValue);
                ps.SpawnPickUpFromEnemy(this.gameObject);
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
        GameObject effect = Instantiate(deathAnimation, transform.position, Quaternion.identity); //Quaternion.identity = no rotation
        Destroy(effect, 0.3f); //hit effect tuhoutuu 0.1sek
    }

    private void BloodEffect()
    {
            GameObject effect = Instantiate(deathAnimation, transform.position, Quaternion.identity); //Quaternion.identity = no rotation
            GameObject burst = Instantiate(bloodParticleEffect, transform.position, Quaternion.identity);
            GameObject splatter = Instantiate(bloodSplatter, transform.position, Quaternion.identity);
            Destroy(effect, 0.2f); //hit effect tuhoutuu 0.1sek
    }

    public void DamagePlayerOnCollision(Collision2D other)
    {
            PlayerStats playerStats = GameManager.instance.PlayerStats;

        if (playerStats.IsGodModeUp || playerStats.IsInvulnerable)
        {
            //do nothing
        }
        else if (playerStats.IsShieldUp && !playerStats.IsGodModeUp)
        {
            TakeDamage(hp, true);
            other.gameObject.GetComponent<PlayerController>().TurnOffShieldGraphic();
            AudioManager.instance.PlaySound(17); //shield break
            other.gameObject.GetComponent<PlayerController>().InvulnerabilityTimer();
            playerStats.IsShieldUp = false;
        } else
        {
            TakeDamage(hp, true);
            other.gameObject.GetComponent<PlayerController>().TurnOffHexDamageEffect();
            other.gameObject.GetComponent<PlayerController>().TurnOffInfiniteDashEffect();
            other.gameObject.GetComponent<PlayerController>().TurnOffInfiniteAmmoEffect();
            playerStats.TakeDamage(damage);
        }
    }

    void EnemyMovementTowardsPlayer()
    {

        playerEnemyDistance = Vector2.Distance(player.transform.position, this.transform.position);
        int multiplier;

        if (shooterEnemy)
        {
            multiplier = -1;
        }
        else
        {
            multiplier = 1;
        }

        if (!shooterEnemy || (shooterEnemy && playerEnemyDistance < retreatDistance))
        {
            direction = player.transform.position - transform.position; //direction vector to player position
            direction.Normalize(); //convert to unit vector
            transform.Translate(direction * multiplier * moveSpeed * Time.deltaTime);
        }
        else if (shooterEnemy)
        {

            if (playerEnemyDistance > stopDistance) //liian kaukana
            {
                direction = player.transform.position - transform.position; //direction vector to player position
                direction.Normalize(); //convert to unit vector

                transform.Translate(direction * moveSpeed * Time.deltaTime);

                //lähestytään pelaajaa
            }
            else if (playerEnemyDistance > stopDistance && playerEnemyDistance > retreatDistance) //sopiva etäisyys
            {
                transform.position = transform.position;
                //pysähdytään
            }
            /*
            else if (playerEnemyDistance < retreatDistance)
            {
                direction = player.transform.position - transform.position; //direction vector to player position
                direction.Normalize(); //convert to unit vector

                transform.Translate(direction * -moveSpeed * Time.deltaTime);
                //paetaan
            }
            */

        }
    }

}
