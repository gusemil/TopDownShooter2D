﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameManager : MonoBehaviour
{
    private PlayerStats playerStats;
    private Pause pause;
    private WeaponSystem weaponSystem;
    private EnemyWaves enemyWaves;
    private Dash dash;
    private bool isGameOver;
    private int lives;
    private float gameTime = 0f;
    private float respawnTime;
    private int points;

    public static GameManager instance; //singleton pattern
    public PlayerStats PlayerStats { get { return playerStats; } }
    public Pause Pause { get { return pause; } }
    public WeaponSystem WeaponSystem { get { return weaponSystem; } }
    public EnemyWaves EnemyWaves { get { return enemyWaves; } }
    public int Lives { get { return lives; } }
    public Dash Dash { get { return dash; } }
    public GameObject playerObject;

    public bool IsGameOver
    {
        get { return isGameOver; }
        set { isGameOver = value; }
    }

    public float GameTime { get { return gameTime; } }
    public int Points { get { return points; } set { points = value; } }

    void Awake()
    {
        if (instance == null) //jos status olio ei ole olemassa
        {
            //DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        playerStats = new PlayerStats();
        pause = new Pause();
        weaponSystem = new WeaponSystem();
        dash = new Dash();
        enemyWaves = new EnemyWaves();
        lives = 3;
        respawnTime = 1f;

        isGameOver = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnGUI()
    {
        GUI.Label(new Rect(20, 10, 200, 20), "Pistol (1) " + weaponSystem.WeaponList[0].Ammo);
        GUI.Label(new Rect(20, 30, 200, 20), "Machine Gun (2) " + weaponSystem.WeaponList[1].Ammo);
        GUI.Label(new Rect(20, 50, 200, 20), "Shotgun (3) " + weaponSystem.WeaponList[2].Ammo);
        GUI.Label(new Rect(20, 70, 200, 20), "Rocket Launcher (4) " + weaponSystem.WeaponList[3].Ammo);
        GUI.Label(new Rect(20, 90, 200, 20), "FlameThrower (5) " + weaponSystem.WeaponList[4].Ammo);
        GUI.Label(new Rect(20, 110, 200, 20), "Bombs (Right Click) " + weaponSystem.BombCount);
        GUI.Label(new Rect(40, 150, 300, 40), "ESC to Pause, 'R' to Retry, SPACE to Dash");
        GUI.Label(new Rect(20, 170, 200, 40), "Lives: " + lives);
        GUI.Label(new Rect(20, 190, 200, 40), "Pause state: " + pause.IsPause);
        GUI.Label(new Rect(20, 210, 200, 40), "Dashes: " + dash.Dashes);
        GUI.Label(new Rect(20, 230, 200, 40), "Dashtimer: " + dash.DashTimer);
        GUI.Label(new Rect(20, 250, 200, 40), "Game Time: " + gameTime);
        GUI.Label(new Rect(20, 390, 200, 40), "Points: " + points);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver)
        {
            gameTime += Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                pause.TogglePause();
            }
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            if (pause.IsPause)
            {
                pause.TogglePause();
            }
            isGameOver = false;
            enemyWaves.CrabSpawnTimer = 0;
            enemyWaves.JumperSpawnTimer = 0;
            enemyWaves.OctopusSpawnTimer = 0;

            lives = 3;
            playerStats = new PlayerStats(); //reset player stats
            SceneManager.LoadScene(0);   
        }
    }

    public void GameOver()
    {
            isGameOver = true;
            GameManager.instance.Pause.TogglePause();
    }

    public void LoseLife()
    {

        lives--;
        if (lives > 0)
        {
            Debug.Log("life lost" + lives);

            StartCoroutine(Respawn());

        } else
        {
            GameOver();
        }
    }

    private IEnumerator Respawn()
    {
        Debug.Log("RESPAWN FUCNTION!");

            PlayerController pc = playerObject.GetComponent<PlayerController>();

            playerObject.GetComponent<SpriteRenderer>().enabled = false;
            playerObject.GetComponent<Collider2D>().enabled = false;

            float originalSpeed = pc.MoveSpeed;
            pc.MoveSpeed = 0f;

            weaponSystem.BombCount++;
            weaponSystem.Bomb();
            PlayerStats.Hp = 1;

            yield return new WaitForSeconds(respawnTime);

            pc.MoveSpeed = originalSpeed;
            playerObject.GetComponent<SpriteRenderer>().enabled = true;
            playerObject.GetComponent<Collider2D>().enabled = true;
    }
    
}
