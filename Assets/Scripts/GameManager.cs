using System.Collections;
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
    private bool isGameOver;
    private int lives;
    private float respawnTime;
    private bool isRespawning;

    public static GameManager instance;
    public PlayerStats PlayerStats { get { return playerStats; } }
    public Pause Pause { get { return pause; } }
    public WeaponSystem WeaponSystem { get { return weaponSystem; } }
    public int Lives { get { return lives; } }
    public GameObject playerObject;

    public bool IsGameOver
    {
        get { return isGameOver; }
        set { isGameOver = value; }
    }

    public GameObject crab;
    public GameObject jumper;
    public GameObject octopus;

    private float crabSpawnTimer = 0;
    private float crabSpawnRate = 0.5f; //0.5f
    private float jumperSpawnTimer = 0;
    private float jumperSpawnRate = 5f; //5f
    private float octopusSpawnTimer = 0;
    private float octopusSpawnRate = 3f; //3f

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
        lives = 3;
        respawnTime = 1f;

        isGameOver = false;
        isRespawning = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("respawn time" + respawnTime);
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

        if (Input.GetKeyUp(KeyCode.R))
        {
            if (pause.IsPause)
            {
                pause.TogglePause();
            }
            isGameOver = false;
            crabSpawnTimer = 0;
            jumperSpawnTimer = 0;
            octopusSpawnTimer = 0;
            lives = 3;
            playerStats = new PlayerStats(); //reset player stats
            SceneManager.LoadScene(0);   
        }
    }

    void SpawnEnemy(GameObject enemyType)
    {
        //Lista vihollisista myöhemmin
        Instantiate(enemyType, transform.position, Quaternion.identity); //Quaternion.identity = no rotation
    }

    public void GameOverCheck()
    {
        if(lives < 0)
        {
            isGameOver = true;
            GameManager.instance.Pause.TogglePause();
        }
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
            GameOverCheck();
        }
    }

    private IEnumerator Respawn()
    {
        isRespawning = true;

        Debug.Log("RESPAWN FUCNTION!");


        //while (isRespawning)
        //{

            PlayerController pc = playerObject.GetComponent<PlayerController>();
            //pc.ChangePlayerColor(pc.PowerUpColor);
            //Debug.Log("powerup color" + pc.PowerUpColor);
            playerObject.GetComponent<SpriteRenderer>().enabled = false;
            playerObject.GetComponent<Collider2D>().enabled = false;

            float originalSpeed = pc.MoveSpeed;
            pc.MoveSpeed = 0f;

            weaponSystem.BombCount++;
            weaponSystem.Bomb();
            PlayerStats.Hp = 1;
            //PlayerStats.IsInvulnerable = true;
            crabSpawnTimer = -3f;
            jumperSpawnTimer = -3f;
            octopusSpawnTimer = -3f;

            yield return new WaitForSeconds(respawnTime);

            //pc.ChangePlayerColor(pc.OriginalColor);
            pc.MoveSpeed = originalSpeed;
            playerObject.GetComponent<SpriteRenderer>().enabled = true;
            playerObject.GetComponent<Collider2D>().enabled = true;
            isRespawning = false;
        //}
    }
    
}
