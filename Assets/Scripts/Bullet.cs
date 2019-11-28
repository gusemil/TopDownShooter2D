using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject hitEffect;

    private float bulletLifeTime;
    private bool isBulletAlive;
    private int bulletDamage;
    private int weaponDamage;

    void Start()
    {
        PlayerStats playerStats = GameManager.status.PlayerStats;
        weaponDamage = 0;
        bulletDamage = playerStats.Damage + weaponDamage;

        bulletLifeTime = 2f;
        isBulletAlive = true;

        StartCoroutine(BulletGoing());
    }

    private IEnumerator BulletGoing()
    {
        while (isBulletAlive == true)
        {
            yield return new WaitForSeconds(bulletLifeTime);
            isBulletAlive = false;
        }

        if (!isBulletAlive)
        {
            Destroy(gameObject);
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity); //Quaternion.identity = no rotation
        Destroy(effect, 0.1f); //hit effect tuhoutuu 0.1sek
        Destroy(gameObject); //tuhotaan bullet collisionissa

        if (collision.transform.GetComponent<Enemy>())
        {
            Enemy enemy = collision.transform.GetComponent<Enemy>();
            Debug.Log("Enemy hit!");
            enemy.TakeDamage(bulletDamage);
        }
    }
}
