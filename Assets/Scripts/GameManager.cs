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
    private EnemyWaves enemyWaves;
    private Dash dash;
    private PickupSystem pickupSystem;
    private UIManager uiManager;
    private bool isGameOver;
    private int lives;
    private float gameTime = 0f;
    private float respawnTime;
    private int points;
    private int pointsMultiplier;
    private bool levelComplete;

    public static GameManager instance; //singleton pattern
    public PlayerStats PlayerStats { get { return playerStats; } }
    public Pause Pause { get { return pause; } }
    public WeaponSystem WeaponSystem { get { return weaponSystem; } }
    public EnemyWaves EnemyWaves { get { return enemyWaves; } }
    public PickupSystem PickupSystem { get { return pickupSystem; } }
    //public UIManager UIManager { get { return uiManager; } }
    public int Lives { get { return lives; } }
    public Dash Dash { get { return dash; } }
    public GameObject playerObject;

    public bool IsGameOver
    {
        get { return isGameOver; }
        set { isGameOver = value; }
    }

    public float GameTime { get { return gameTime; } }
    public bool LevelComplete { get { return levelComplete; } }
    public int Points { get { return points; } set { points = value; } }
    public int PointsMultiplier { get { return pointsMultiplier; }
        set { pointsMultiplier = value; } }

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
        pickupSystem = new PickupSystem();
        
        lives = 3;
        respawnTime = 1f;
        points = 0;
        PointsMultiplier = 1;
        isGameOver = false;
        Time.timeScale = 1;
        levelComplete = false;
        //pause.IsPause = false
        //Time.timeScale
    }

    // Start is called before the first frame update
    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        uiManager.UpdateScore(instance);
        uiManager.UpdatePointMultiplierText(instance);

        Debug.Log(LevelManager.instance.CurrentLevel);
    }

    void OnGUI()
    {
        /*
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
        */
        GUI.Label(new Rect(20, 470, 200, 40), "HexDamageOn " + playerStats.IsHexDamageUp);
        GUI.Label(new Rect(20, 490, 200, 40), "InfiniteAmmoOn: " + playerStats.IsInfiniteAmmoUp);
        GUI.Label(new Rect(20, 510, 200, 40), "PointsMultiplier " + pointsMultiplier);
        GUI.Label(new Rect(20, 530, 200, 40), "PlayerStats ShieldUp " + playerStats.IsShieldUp);
    }

    // Update is called once per frame
    void Update()
    { 
        if (Input.GetKeyDown(KeyCode.Escape) && (pause.IsPause))
        {
            //pause.TogglePause();
            //uiManager.ToggleGameOverScreen(instance);
            pause.TogglePause();
            SceneManager.LoadScene("MainMenu");
        }

        if (!isGameOver)
        {
            gameTime += Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.Escape) && !pause.IsPause)
            {
                //pause.TogglePause();
                //uiManager.ToggleGameOverScreen(instance);
                pause.TogglePause();
                uiManager.TogglePauseText(pause);
            }

            /*
            else if (Input.GetKeyDown(KeyCode.Escape) && isGameOver)
            {
                //pause.TogglePause();
                SceneManager.LoadScene("MainMenu");
            }
            */

            else if (Input.GetKeyDown(KeyCode.Space) && pause.IsPause)
            {
                //pause.TogglePause();
                //uiManager.ToggleGameOverScreen(instance);
                pause.TogglePause();
                uiManager.TogglePauseText(pause);
            }
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            Restart();
        }
    }

    public void GameOver()
    {
            uiManager.ShowGameOverScreen();
            isGameOver = true;
            GameManager.instance.Pause.TogglePause();
    }

    public void LoseLife()
    {

        lives--;
        if (lives >= 0)
        {
            uiManager.UpdateLives(instance);
            StartCoroutine(Respawn());

        } else
        {
            GameOver();
        }
    }

    public void AddPoints(int pointsToBeAdded)
    {
        points += pointsToBeAdded * pointsMultiplier;
        uiManager.UpdateScore(instance);
    }

    private IEnumerator Respawn()
    {
            PlayerController pc = playerObject.GetComponent<PlayerController>();

            playerObject.GetComponent<SpriteRenderer>().enabled = false;
            playerObject.GetComponent<Collider2D>().enabled = false;

        playerStats.IsRespawning = true;

            float originalSpeed = pc.MoveSpeed;
            pc.MoveSpeed = 0f;

            weaponSystem.BombCount++;
            weaponSystem.Bomb();
            PlayerStats.Hp = 1;
        playerStats.IsHexDamageUp = false;
        playerStats.IsInfiniteAmmoUp = false;
        playerStats.IsInfiniteDashUp = false;
        playerStats.DamageMultiplier = 1;
        pointsMultiplier = 1;
        pc.ChangePlayerColor(pc.OriginalColor);

        uiManager.UpdatePointMultiplierText(instance);
        uiManager.UpdateWeaponText(weaponSystem.CurrentWeapon);
        uiManager.UpdateWeaponImage(weaponSystem.CurrentWeapon);
        uiManager.UpdateBombText(weaponSystem);

        yield return new WaitForSeconds(respawnTime);

            pc.MoveSpeed = originalSpeed;
            playerObject.GetComponent<SpriteRenderer>().enabled = true;
            playerObject.GetComponent<Collider2D>().enabled = true;
        playerStats.IsRespawning = false;
    }

    private void Restart()
    {
        if (pause.IsPause)
        {
            pause.TogglePause();
        }
        //uiManager.ToggleGameOverScreen(instance);
        isGameOver = false;
        enemyWaves.CrabSpawnTimer = 0;
        enemyWaves.JumperSpawnTimer = 0;
        enemyWaves.OctopusSpawnTimer = 0;

        lives = 3;
        weaponSystem.BombCount = 1;
        playerStats = new PlayerStats(); //reset player stats
        weaponSystem.ChangeWeapon(0);
        uiManager.UpdatePointMultiplierText(instance);
        uiManager.UpdateWeaponText(weaponSystem.CurrentWeapon);
        uiManager.UpdateWeaponImage(weaponSystem.CurrentWeapon);
        uiManager.UpdateBombText(weaponSystem);
        SceneManager.LoadScene(1);
    }

    public IEnumerator CompleteLevel()
    {
        Debug.Log("winner is you");
        levelComplete = true;
        StartCoroutine(uiManager.ShowLevelCompleteText(enemyWaves));
        //pause.TogglePause();
        yield return new WaitForSeconds(4f);
        //pause.TogglePause();
        GameOver();
    }



}
