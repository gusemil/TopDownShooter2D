﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject hitEffect;
    public GameObject explosion;
    public Sprite missile;
    public Sprite flame;

    private bool isProjectileAlive;
    private int totalProjectileDamage;
    private float SplashDamageRadius;
    private float projectileLifeTime;
    private string nameOfWeaponShot;
    private WeaponSystem weapon;

    private void Awake()
    {
        PlayerStats playerStats = GameManager.instance.PlayerStats;
        weapon = GameManager.instance.WeaponSystem;
        totalProjectileDamage = playerStats.DamageMultiplier * weapon.CurrentWeapon.WeaponDamage;
        SplashDamageRadius = weapon.CurrentWeapon.SplashDamageRadius;
        projectileLifeTime = weapon.CurrentWeapon.ProjectileLifeTime;
        nameOfWeaponShot = weapon.CurrentWeapon.WeaponName;
        isProjectileAlive = true;

        if (nameOfWeaponShot == "Rocket Launcher")
        {
            GetComponent<SpriteRenderer>().sprite = missile;
            transform.localScale = new Vector3(transform.localScale.x / 2f, transform.localScale.x / 3f, transform.localScale.x / 2f);
        }


        if (nameOfWeaponShot == "Flamethrower")
        {
            GetComponent<SpriteRenderer>().sprite = flame;
            transform.localScale = new Vector3(transform.localScale.x * 0.6f, transform.localScale.x * 0.6f, transform.localScale.x * 0.6f);
        }

        if (nameOfWeaponShot == "Shotgun")
        {
            transform.localScale = new Vector3(transform.localScale.x * 1.5f, transform.localScale.x * 1.5f, transform.localScale.x * 1.5f);
        }


        if (playerStats.IsGodModeUp && nameOfWeaponShot != "Rocket Launcher")
        {
            transform.localScale = new Vector3(transform.localScale.x * 3f, transform.localScale.x * 3f, transform.localScale.x * 3f);
        }


    }
    void Start()
    {
        StartCoroutine(ProjectileGoing());
    }

    private IEnumerator ProjectileGoing()
    {
        while (isProjectileAlive == true)
        {
            yield return new WaitForSeconds(projectileLifeTime);
            isProjectileAlive = false;
        }

        if (!isProjectileAlive)
        {
            Destroy(gameObject);
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject effect;

        if (nameOfWeaponShot != "Rocket Launcher")
        {
            effect = Instantiate(hitEffect, transform.position, Quaternion.identity); //Quaternion.identity = no rotation
        }
        else
        {
            effect = Instantiate(explosion, transform.position, Quaternion.identity); //Quaternion.identity = no rotation
            weapon.PlayRocketExplosionSound();
        }

        Destroy(effect, 0.2f);
        Destroy(gameObject);

        if (collision.transform.GetComponent<Enemy>())
        {
            Enemy enemy = collision.transform.GetComponent<Enemy>();
            enemy.TakeDamage(totalProjectileDamage, false);
        }

        //Splash damage
        if (SplashDamageRadius > 0)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            foreach (GameObject enemy in enemies)
            {
                if (SplashDamageRadius >= Vector2.Distance(transform.position, enemy.transform.position))
                {
                    enemy.GetComponent<Enemy>().TakeDamage(totalProjectileDamage, false);
                }
            }
        }
    }
}
