using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    private int weaponIndex;
    private float bombSpawnDelay = -1f;
    private PlayerStats stats;
    //private UIManager uiManager;

    public static int bombCount;
    public static Weapon currentWeapon;
    public static List<Weapon> weaponList;
    public static float shotTimer;

    public float ShotTimer { get { return shotTimer; } set { shotTimer = value; } }
    public Weapon CurrentWeapon { get { return currentWeapon; } set { currentWeapon = value; } }
    public List<Weapon> WeaponList { get { return weaponList; } set { weaponList = value; } }
    public int BombCount { get { return bombCount; } set { bombCount = value; } }

    Weapon pistol = new Weapon("pistol", 0, 50, 20f, 0.3f, 1, 0f, 2f); //name, number, dmg, force, fireRate, ammo, radius, lifetime
    Weapon machineGun = new Weapon("machinegun", 1, 20, 40f, 0.05f, 500, 0f, 2f);
    Weapon shotgun = new Weapon("shotgun", 2, 200, 100f, 0.3f, 50, 5f, 3f);
    Weapon rocketLauncher = new Weapon("rocketlauncher", 3, 500, 7f, 0.5f, 5, 12f, 5f);
    Weapon flameThrower = new Weapon("flameThrower", 4, 50, 30f, 0.01f, 100, 2f, 0.3f);



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

        stats = GameManager.instance.PlayerStats;
        //uiManager = FindObjectOfType<UIManager>();
        //ui = FindObjectOfType<UIManager>();
        /*
        if(FindObjectOfType<UIManager>() == true)
        {
            Debug.Log("UI found!");
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        shotTimer += Time.deltaTime;

        /*
        if (Input.GetKeyUp(KeyCode.E))
        {
            ChangePreviousWeapon();
        }

        if (Input.GetKeyUp(KeyCode.Q))
        {
            ChangeNextWeapon();
        }
        */ 
    }

    /*
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
    */

    public void ChangeWeapon(int weaponChoice)
    {
        weaponIndex = weaponChoice;
        currentWeapon = weaponList[weaponIndex];

        //uiManager.UpdateWeaponText(GameManager.instance.WeaponSystem.CurrentWeapon);
    }

    public void LoseAmmo()
    {
        if (currentWeapon.Ammo > 0 && currentWeapon.WeaponName != "pistol")
        {
            currentWeapon.Ammo--;
        }

        //uiManager.UpdateWeaponText(GameManager.instance.WeaponSystem.CurrentWeapon);
    }

    public void Bomb()
    {
        if (bombCount > 0)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            foreach (GameObject enemy in enemies)
            {
                enemy.GetComponent<Enemy>().TakeDamage(500,true);
            }

            EnemyWaves ew = GameManager.instance.EnemyWaves;

            if (!ew.IsSpawningPaused)
            {
               ew.CrabSpawnTimer = bombSpawnDelay;
               ew.JumperSpawnTimer = bombSpawnDelay;
               ew.OctopusSpawnTimer = bombSpawnDelay;
            }

            LoseBomb();

            if (ew.IsSpawningPaused && ew.EnemiesAlive == 0 && ew.EnemiesSpawned == 0)
            {
                ew.NextWave();
            }
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
