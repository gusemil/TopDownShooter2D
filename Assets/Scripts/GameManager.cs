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
    private bool isGameOver;

    public static GameManager status; //miten tämä on singleton?
    public PlayerStats PlayerStats { get { return playerStats; } }
    public Pause Pause { get { return pause; } }

    public bool IsGameOver
    {
        get { return isGameOver; }
        set { isGameOver = value; }
    }

    public GameObject enemy;

    private float spawnTimer = 0;
    private float enemySpawnRate = 2f;

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

            spawnTimer += Time.deltaTime;

            if (spawnTimer >= enemySpawnRate)
            {
                SpawnEnemy();
                spawnTimer = 0;
            }
        }
    }

    void SpawnEnemy()
    {
        //Lista vihollisista myöhemmin
        Instantiate(enemy, transform.position, Quaternion.identity); //Quaternion.identity = no rotation
    }
    
}
