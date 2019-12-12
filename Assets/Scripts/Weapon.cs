using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon
{
    private string _weaponName;
    private int _weaponNumber;
    private float _bulletForce;
    private float _fireCoolDown;
    private int _weaponDamage;
    private int _ammo;
    private float _splashDamageRadius;
    private float _projectileLifeTime;

    public string WeaponName { get { return _weaponName; } set { _weaponName = value; } }
    public int WeaponNumber { get { return _weaponNumber; } set { _weaponNumber = value; } }
    public float BulletForce { get { return _bulletForce; } set { _bulletForce = value; } }
    public float FireCoolDown { get { return _fireCoolDown; } set { _fireCoolDown = value; } }
    public int WeaponDamage { get { return _weaponDamage; } set { _weaponDamage = value; } }
    public int Ammo { get { return _ammo; } set { _ammo = value; } }
    public float SplashDamageRadius { get { return _splashDamageRadius; } set { _splashDamageRadius = value; } }
    public float ProjectileLifeTime { get { return _projectileLifeTime; } set { _projectileLifeTime = value; } }

    public Weapon(string weaponName, int weaponNumber, int weaponDamage, float bulletForce, float fireCoolDown, int ammo, float splashDamageRadius, float projectileLifeTime)
    {
        _weaponName = weaponName;
        _weaponNumber = weaponNumber;
        _weaponDamage = weaponDamage;
        _bulletForce = bulletForce;
        _fireCoolDown = fireCoolDown;
        _ammo = ammo;
        _splashDamageRadius = splashDamageRadius;
        _projectileLifeTime = projectileLifeTime;
    }

}
