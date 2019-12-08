using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    private int weaponIndex;

    public static Weapon currentWeapon;
    public static List<Weapon> weaponList;
    public static float shotTimer;

    public float ShotTimer { get { return shotTimer; } set { shotTimer = value; } }
    public Weapon CurrentWeapon { get { return currentWeapon; } set { currentWeapon = value; } }
    public List<Weapon> WeaponList { get { return weaponList; } set { weaponList = value; } }

    Weapon pistol = new Weapon("pistol", 0, 50, 20f, 0.3f, 100);
    Weapon machineGun = new Weapon("machinegun", 1, 10, 40f, 0.05f, 100);
    Weapon shotgun = new Weapon("shotgun", 2, 200, 100f, 1f, 5);

    // Start is called before the first frame update
    void Start()
    {
        weaponList = new List<Weapon>();

        weaponList.Add(pistol);
        weaponList.Add(machineGun);
        weaponList.Add(shotgun);

        weaponIndex = 0;
        currentWeapon = weaponList[0];
        shotTimer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        shotTimer += Time.deltaTime;
    }

    public void ChangePreviousWeapon()
    {
        weaponIndex--;

        if (weaponIndex < 0)
            weaponIndex = weaponList.Count - 1;

        currentWeapon = weaponList[weaponIndex];
    }

    public void ChangeNextWeapon()
    {
        weaponIndex++;

        if (weaponIndex > WeaponList.Count - 1)
            weaponIndex = 0;

        currentWeapon = weaponList[weaponIndex];
    }

    public void ChangeWeapon(int weaponChoice)
    {
        weaponIndex = weaponChoice;
        currentWeapon = weaponList[weaponIndex];
    }

    public void loseAmmo()
    {
        if (currentWeapon.Ammo > 0)
        {
            currentWeapon.Ammo--;
            Debug.Log(currentWeapon.WeaponName + " ammo count: " + currentWeapon.Ammo);
        }
    }

}
