using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    Weapon pistol = new Weapon("machinegun", 0, 20f, 0.1f );
    Weapon machineGun = new Weapon("machinegun", 1, 40f, 0.05f);

    public static Weapon currentWeapon;
    public static List<Weapon> weaponList;
    public static float shotTimer;

    public float ShotTimer { get { return shotTimer; } set { shotTimer = value; } }
    public Weapon CurrentWeapon { get { return currentWeapon; } set { currentWeapon = value; } }
    public List<Weapon> WeaponList { get { return weaponList; } set { weaponList = value; } }

    public void ChangeWeapon()
    {
        //currentWeapon 
    }

    // Start is called before the first frame update
    void Start()
    {
        weaponList = new List<Weapon>();

        weaponList.Add(pistol);
        weaponList.Add(machineGun);

        currentWeapon = weaponList[0];
        shotTimer = 0f;

        //Debug.Log("Weapon count: " + weaponList.Count);

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(timer);
        ShotTimer += Time.deltaTime;
    }
}
