using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameManager : MonoBehaviour
{
    private PlayerStats playerStats;
    private Pause pause;
    private WeaponSystem weaponSystem;
    private bool isGameOver;

    public static GameManager status;
    public PlayerStats PlayerStats { get { return playerStats; } }
    public Pause Pause { get { return pause; } }
    public WeaponSystem WeaponSystem { get { return weaponSystem; } }

    public bool IsGameOver
    {
        get { return isGameOver; }
        set { isGameOver = value; }
    }

    public GameObject crab;
    public GameObject jumper;
    public GameObject octopus;

    private float crabSpawnTimer = 0;
    private float crabSpawnRate = 180f; //2f
    private float jumperSpawnTimer = 0;
    private float jumperSpawnRate = 180f; //5f
    private float octopusSpawnTimer = 0;
    private float octopusSpawnRate = 3f; //3f

    void Awake()
    {
        if (status == null) //jos status olio ei ole olemassa
        {
            DontDestroyOnLoad(gameObject);
            status = this;
        }
        else
        {
            Destroy(gameObject);
        }

        playerStats = new PlayerStats();
        pause = new Pause();
        weaponSystem = new WeaponSystem();

        isGameOver = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                pause.TogglePause();
            }

            crabSpawnTimer += Time.deltaTime;
            jumperSpawnTimer += Time.deltaTime;
            octopusSpawnTimer += Time.deltaTime;

            if (crabSpawnTimer >= crabSpawnRate)
            {
                SpawnEnemy(crab);
                crabSpawnTimer = 0;
            }

            if (jumperSpawnTimer >= jumperSpawnRate)
            {
                SpawnEnemy(jumper);
                jumperSpawnTimer = 0;
            }

            if (octopusSpawnTimer >= octopusSpawnRate)
            {
                SpawnEnemy(octopus);
                octopusSpawnTimer = 0;
            }
        }
    }

    void SpawnEnemy(GameObject enemyType)
    {
        //Lista vihollisista myöhemmin
        Instantiate(enemyType, transform.position, Quaternion.identity); //Quaternion.identity = no rotation
    }
    
}
