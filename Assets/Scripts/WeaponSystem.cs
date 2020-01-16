using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    private int weaponIndex;
    private float bombSpawnDelay = -1f;
    private float gunVolume = 0.3f;
    private float heavyGunVolume = 0.5f;

    public static int bombCount;
    public static Weapon currentWeapon;
    public static List<Weapon> weaponList;
    public static float shotTimer;

    public float ShotTimer { get { return shotTimer; } set { shotTimer = value; } }
    public Weapon CurrentWeapon { get { return currentWeapon; } set { currentWeapon = value; } }
    public List<Weapon> WeaponList { get { return weaponList; } set { weaponList = value; } }
    public int BombCount { get { return bombCount; } set { bombCount = value; } }

    Weapon pistol = new Weapon("Pistol", 0, 50, 20f, 0.3f, 1, 0f, 2f, 0); //name, number, dmg, force, fireRate, ammo on start, radius, lifetime, ammo on pickup
    Weapon machineGun = new Weapon("Machine Gun", 1, 30, 40f, 0.13f, 100, 0f, 2f, 100);
    Weapon shotgun = new Weapon("Shotgun", 2, 200, 100f, 0.3f, 25, 5f, 1f, 25);
    Weapon rocketLauncher = new Weapon("Rocket Launcher", 3, 500, 7f, 0.75f, 5, 12f, 9f, 2);
    Weapon flameThrower = new Weapon("Flamethrower", 4, 100, 30f, 0.01f, 75, 1f, 0.3f, 75);

    void Start()
    {
        weaponList = new List<Weapon>();

        weaponList.Add(pistol);
        weaponList.Add(machineGun);
        weaponList.Add(shotgun);

        if (LevelManager.instance.CurrentLevel >= 2)
        {
            WeaponList.Add(rocketLauncher);
        }

        if (LevelManager.instance.CurrentLevel >= 3)
        {
            WeaponList.Add(flameThrower);
        }

        weaponIndex = 0;
        currentWeapon = weaponList[0];
        shotTimer = 0f;

        bombCount = 1;
    }

    void Update()
    {
        shotTimer += Time.deltaTime;
    }

    public void ChangeWeapon(int weaponChoice)
    {
        weaponIndex = weaponChoice;
        currentWeapon = weaponList[weaponIndex];
    }

    public void LoseAmmo()
    {
        if (currentWeapon.Ammo > 0 && currentWeapon.WeaponName != "Pistol")
        {
            currentWeapon.Ammo--;
        }
    }

    public void Bomb()
    {
        if (bombCount > 0)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            foreach (GameObject enemy in enemies)
            {
                enemy.GetComponent<Enemy>().TakeDamage(500, true);
            }

            EnemyWaves ew = GameManager.instance.EnemyWaves;

            if (!ew.IsSpawningPaused)
            {
                ew.CrabSpawnTimer = bombSpawnDelay;
                ew.JumperSpawnTimer = bombSpawnDelay;
                ew.OctopusSpawnTimer = bombSpawnDelay;
            }

            LoseBomb();
            PlayBombSound();
        }
    }

    public void LoseBomb()
    {
        if (bombCount > 0)
        {
            BombCount--;
        }
    }


    public void PlayWeaponSound(string weaponName)
    {
        if (weaponName == "Pistol")
            AudioManager.instance.PlaySound(0, gunVolume);
        else if (weaponName == "Machine Gun")
            AudioManager.instance.PlaySound(1, gunVolume);
        else if (weaponName == "Shotgun")
            AudioManager.instance.PlaySound(2, heavyGunVolume);
        else if (weaponName == "Rocket Launcher")
            AudioManager.instance.PlaySound(3, heavyGunVolume);
        else if (weaponName == "Flamethrower")
            AudioManager.instance.PlaySound(4, gunVolume);
    }

    public void PlayBombSound()
    {
        AudioManager.instance.PlaySound(5);
    }

    public void PlayRocketExplosionSound()
    {
        AudioManager.instance.PlaySound(6, heavyGunVolume);
    }

    public void PlayOutOfAmmoSound()
    {
        AudioManager.instance.PlaySound(28, gunVolume);
    }


}
