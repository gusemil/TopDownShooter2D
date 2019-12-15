using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    private int weaponIndex;


    public static int bombCount;
    public static Weapon currentWeapon;
    public static List<Weapon> weaponList;
    public static float shotTimer;

    public float ShotTimer { get { return shotTimer; } set { shotTimer = value; } }
    public Weapon CurrentWeapon { get { return currentWeapon; } set { currentWeapon = value; } }
    public List<Weapon> WeaponList { get { return weaponList; } set { weaponList = value; } }
    public int BombCount { get { return bombCount; } set { bombCount = value; } }

    Weapon pistol = new Weapon("pistol", 0, 50, 20f, 0.3f, 1, 0f, 2f); //name, number, dmg, force, fireRate, ammo, radius, lifetime
    Weapon machineGun = new Weapon("machinegun", 1, 10, 40f, 0.05f, 100, 0f, 2f);
    Weapon shotgun = new Weapon("shotgun", 2, 200, 100f, 0.6f, 10, 2f, 2f);
    Weapon rocketLauncher = new Weapon("rocketlauncher", 3, 500, 7f, 0.6f, 5, 10f, 5f);
    Weapon flameThrower = new Weapon("flameThrower", 4, 100, 30f, 0.01f, 1000, 2f, 0.3f);



    // Start is called before the first frame update
    void Start()
    {
        weaponList = new List<Weapon>();

        weaponList.Add(pistol);
        weaponList.Add(machineGun);
        weaponList.Add(shotgun);
        WeaponList.Add(rocketLauncher);
        WeaponList.Add(flameThrower);

        weaponIndex = 0;
        currentWeapon = weaponList[0];
        shotTimer = 0f;

        bombCount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        shotTimer += Time.deltaTime;


        if (Input.GetKeyUp(KeyCode.E))
        {
            ChangePreviousWeapon();
        }

        if (Input.GetKeyUp(KeyCode.Q))
        {
            ChangeNextWeapon();
        }

        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            ChangeWeapon(0);
        }

        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            ChangeWeapon(1);
        }

        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            ChangeWeapon(2);
        }

        if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            ChangeWeapon(3);
        }

        if (Input.GetKeyUp(KeyCode.Alpha5))
        {
            ChangeWeapon(4);
        }
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

    public void LoseAmmo()
    {
        if (currentWeapon.Ammo > 0 && currentWeapon.WeaponName != "pistol" )
        {
            currentWeapon.Ammo--;
            Debug.Log(currentWeapon.WeaponName + " ammo count: " + currentWeapon.Ammo);
        }
    }

    public void Bomb()
    {
        if (bombCount > 0)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            foreach (GameObject enemy in enemies)
            {
                enemy.GetComponent<Enemy>().TakeDamage(1000);
            }

            GameManager gm = GameManager.instance;

            gm.CrabSpawnTimer = -3f;
            gm.JumperSpawnTimer = -3f;
            gm.OctopusSpawnTimer = -3f;

            LoseBomb();
        }
    }

    public void LoseBomb()
    {
        if (bombCount > 0)
        {
            BombCount--;
            Debug.Log(bombCount + " Bombs");
        }
    }

}
