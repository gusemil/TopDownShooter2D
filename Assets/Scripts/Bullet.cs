using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject hitEffect;

    //private float bulletLifeTime;
    private bool isBulletAlive;
    private int bulletDamage;
    //private int weaponDamage;

    private WeaponSystem weapon;

    void Start()
    {
        PlayerStats playerStats = GameManager.status.PlayerStats;
        weapon = GameManager.status.WeaponSystem;
        bulletDamage = playerStats.DamageMultiplier * weapon.CurrentWeapon.WeaponDamage;

        //bulletLifeTime = 2f;
        isBulletAlive = true;

        StartCoroutine(BulletGoing());
    }

    private IEnumerator BulletGoing()
    {
        while (isBulletAlive == true)
        {
            yield return new WaitForSeconds(weapon.CurrentWeapon.ProjectileLifeTime);
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

        if (collision.transform.GetComponent<Enemy>()) //osuu viholliseen
        {
            Enemy enemy = collision.transform.GetComponent<Enemy>();
            enemy.TakeDamage(bulletDamage);

            Debug.Log("osuu vihuun!" + bulletDamage);
        }

        //Splash damage
        if (weapon.CurrentWeapon.SplashDamageRadius > 0)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            foreach (GameObject enemy in enemies)
            {
                if (weapon.CurrentWeapon.SplashDamageRadius >= Vector2.Distance(transform.position, enemy.transform.position))
                {
                    enemy.GetComponent<Enemy>().TakeDamage(weapon.CurrentWeapon.WeaponDamage);
                }
            }
        }
    }
}
