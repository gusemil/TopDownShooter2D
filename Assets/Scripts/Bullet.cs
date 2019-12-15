using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject hitEffect;

    //private float bulletLifeTime;
    private bool isProjectileAlive;
    private int totalProjectileDamage;
    private float localSplashDamageRadius;
    //private int weaponDamage;

    private WeaponSystem weapon;

    void Start()
    {
        PlayerStats playerStats = GameManager.instance.PlayerStats;
        weapon = GameManager.instance.WeaponSystem;
        totalProjectileDamage = playerStats.DamageMultiplier * weapon.CurrentWeapon.WeaponDamage;
        localSplashDamageRadius = weapon.CurrentWeapon.SplashDamageRadius;

        //bulletLifeTime = 2f;
        isProjectileAlive = true;

        StartCoroutine(ProjectileGoing());
    }

    private IEnumerator ProjectileGoing()
    {
        while (isProjectileAlive == true)
        {
            yield return new WaitForSeconds(weapon.CurrentWeapon.ProjectileLifeTime);
            isProjectileAlive = false;
        }

        if (!isProjectileAlive)
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
            enemy.TakeDamage(totalProjectileDamage);

            Debug.Log("osuu vihuun!" + totalProjectileDamage);
        }

        //Splash damage
        if (localSplashDamageRadius > 0)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            foreach (GameObject enemy in enemies)
            {
                if (localSplashDamageRadius >= Vector2.Distance(transform.position, enemy.transform.position))
                {
                    enemy.GetComponent<Enemy>().TakeDamage(totalProjectileDamage);
                }
            }
        }
    }
}
