using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon
{
    private string _weaponName;
    private int _weaponNumber; //0 machine gun, 1 shotgun
    private float _bulletForce; //20f;
    private float _fireCoolDown; //0.1f;

    public string WeaponName { get { return _weaponName; } set { _weaponName = value; } }
    public int WeaponNumber { get { return _weaponNumber; } set { _weaponNumber = value; } }
    public float BulletForce { get { return _bulletForce; } set { _bulletForce = value; } }
    public float FireCoolDown { get { return _fireCoolDown; } set { _fireCoolDown = value; } }

    public Weapon(string weaponName, int weaponNumber, float bulletForce, float fireCoolDown)
    {
        _weaponName = weaponName;
        _weaponNumber = weaponNumber;
        _bulletForce = bulletForce;
        _fireCoolDown = fireCoolDown;
    }

    //private float weaponTimer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //weaponTimer += Time.deltaTime;
    }


}
