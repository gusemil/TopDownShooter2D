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

    public string WeaponName { get { return _weaponName; } set { _weaponName = value; } }
    public int WeaponNumber { get { return _weaponNumber; } set { _weaponNumber = value; } }
    public float BulletForce { get { return _bulletForce; } set { _bulletForce = value; } }
    public float FireCoolDown { get { return _fireCoolDown; } set { _fireCoolDown = value; } }
    public int WeaponDamage { get { return _weaponDamage; } set { _weaponDamage = value; } }
    public int Ammo { get { return _ammo; } set { _ammo = value; } }

    public Weapon(string weaponName, int weaponNumber, int weaponDamage, float bulletForce, float fireCoolDown, int ammo)
    {
        _weaponName = weaponName;
        _weaponNumber = weaponNumber;
        _weaponDamage = weaponDamage;
        _bulletForce = bulletForce;
        _fireCoolDown = fireCoolDown;
        _ammo = ammo;
    }

}
