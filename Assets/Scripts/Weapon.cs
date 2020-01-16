using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon
{
    private string weaponName;
    private int weaponNumber;
    private float bulletForce;
    private float fireCoolDown;
    private int weaponDamage;
    private int ammo;
    private float splashDamageRadius;
    private float projectileLifeTime;
    private int ammoFromPickup;

    public string WeaponName { get { return weaponName; } set { weaponName = value; } }
    public int WeaponNumber { get { return weaponNumber; } set { weaponNumber = value; } }
    public float BulletForce { get { return bulletForce; } set { bulletForce = value; } }
    public float FireCoolDown { get { return fireCoolDown; } set { fireCoolDown = value; } }
    public int WeaponDamage { get { return weaponDamage; } set { weaponDamage = value; } }
    public int Ammo { get { return ammo; } set { ammo = value; } }
    public float SplashDamageRadius { get { return splashDamageRadius; } set { splashDamageRadius = value; } }
    public float ProjectileLifeTime { get { return projectileLifeTime; } set { projectileLifeTime = value; } }
    public int AmmoFromPickup { get { return ammoFromPickup; } set { ammoFromPickup = value; } }

    public Weapon(string weaponName, int weaponNumber, int weaponDamage, float bulletForce, float fireCoolDown, int ammo, float splashDamageRadius, float projectileLifeTime, int ammoFromPickup)
    {
        this.weaponName = weaponName;
        this.weaponNumber = weaponNumber;
        this.weaponDamage = weaponDamage;
        this.bulletForce = bulletForce;
        this.fireCoolDown = fireCoolDown;
        this.ammo = ammo;
        this.splashDamageRadius = splashDamageRadius;
        this.projectileLifeTime = projectileLifeTime;
        this.ammoFromPickup = ammoFromPickup;
    }

}
