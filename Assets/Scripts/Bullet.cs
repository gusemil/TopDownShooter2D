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
         

        //Tee tägeillä mielummin?

        if (collision.transform.GetComponent<Enemy>() && !this.gameObject.GetComponent<Enemy>()) //osuu viholliseen ja scriptin omistaja ei ole vihollinen
        {
            Enemy enemy = collision.transform.GetComponent<Enemy>();
            enemy.TakeDamage(bulletDamage);

            Debug.Log("osuu vihuun!");
        }

        else if (collision.transform.GetComponent<PlayerController>() && !this.gameObject.GetComponent<PlayerController>()) //osuu pelaajaan ja scriptin omistaja ei ole pelaajaa
        {
            PlayerStats player = GameManager.status.PlayerStats;
            player.TakeDamage(bulletDamage);

            Debug.Log("osuu pelaajaan!");
        }
    }
}
