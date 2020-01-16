using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    private PlayerStats playerStats;
    private Pause pause;
    private WeaponSystem weaponSystem;
    private EnemyWaves enemyWaves;
    private Dash dash;
    private PickupSystem pickupSystem;
    private UIManager uiManager;
    private LevelManager lvlManager;
    private bool isGameOver;
    private int lives;
    private float gameTime = 0f;
    private float respawnTime;
    private int points;
    private int pointsMultiplier;
    private bool levelComplete;


    public static GameManager instance; //singleton
    public PlayerStats PlayerStats { get { return playerStats; } }
    public Pause Pause { get { return pause; } }
    public WeaponSystem WeaponSystem { get { return weaponSystem; } }
    public EnemyWaves EnemyWaves { get { return enemyWaves; } }
    public PickupSystem PickupSystem { get { return pickupSystem; } }
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
    public int PointsMultiplier
    {
        get { return pointsMultiplier; }
        set { pointsMultiplier = value; }
    }

    void Awake()
    {
        if (instance == null)
        {
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
    }

    void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        lvlManager = FindObjectOfType<LevelManager>();
        uiManager.UpdateScore(instance);
        uiManager.UpdatePointMultiplierText(instance);
    }

    void Update()
    {
        if (!isGameOver)
        {
            gameTime += Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.Escape) && !pause.IsPause)
            {
                pause.TogglePause();
                uiManager.TogglePauseText(pause);
            }

            else if (Input.GetKeyDown(KeyCode.Escape) && pause.IsPause)
            {
                pause.TogglePause();
                uiManager.TogglePauseText(pause);
            }

            else if (Input.GetKeyDown(KeyCode.Space) && pause.IsPause)
            {
                pause.TogglePause();
                AudioManager.instance.soundSource.Stop();
                SceneManager.LoadScene("MainMenu");
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape) && (pause.IsPause))
            {
                pause.TogglePause();
                AudioManager.instance.soundSource.Stop();
                SceneManager.LoadScene("MainMenu");
            }
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            Restart();
        }
    }

    public IEnumerator GameOver()
    {
        AudioManager.instance.StopMusic();
        AudioManager.instance.PlaySound(23);
        weaponSystem.BombCount++;
        weaponSystem.Bomb();
        playerStats.IsRespawning = true;
        playerObject.GetComponent<SpriteRenderer>().enabled = false;
        playerObject.GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(3f);
        uiManager.ShowGameOverScreen();
        isGameOver = true;
        GameManager.instance.Pause.TogglePause();
    }

    public void LoseLife()
    {

        lives--;
        if (lives >= 0)
        {
            AudioManager.instance.PlaySound(22);
            uiManager.UpdateLives(instance);
            StartCoroutine(Respawn());

        }
        else
        {
            StartCoroutine(GameOver());
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

        float originalSpeed = 7.5f;
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

        UpdateUI();

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
        isGameOver = false;
        enemyWaves.CrabSpawnTimer = 0;
        enemyWaves.JumperSpawnTimer = 0;
        enemyWaves.OctopusSpawnTimer = 0;

        lives = 3;
        weaponSystem.BombCount = 1;
        playerStats = new PlayerStats(); //reset player stats
        weaponSystem.ChangeWeapon(0);
        UpdateUI();
        SceneManager.LoadScene(1);
        AudioManager.instance.musicSource.Play();
    }

    public IEnumerator CompleteLevel()
    {
        levelComplete = true;

        if (lvlManager.HighestUnlockedLevel == 0)
        {
            lvlManager.HighestUnlockedLevel = 1;
        }

        if (lvlManager.HighestUnlockedLevel == lvlManager.CurrentLevel)
        {
            lvlManager.HighestUnlockedLevel++;
            lvlManager.Save(lvlManager);
            lvlManager.Load(lvlManager);
        }

        StartCoroutine(uiManager.ShowLevelCompleteText(enemyWaves));
        AudioManager.instance.PlaySound(25);
        yield return new WaitForSeconds(3f);
        AudioManager.instance.StopMusic();
        uiManager.ShowGameOverScreen();
        isGameOver = true;
        GameManager.instance.Pause.TogglePause();
    }

    private void UpdateUI()
    {
        uiManager.UpdatePointMultiplierText(instance);
        uiManager.UpdateWeaponText(weaponSystem.CurrentWeapon);
        uiManager.UpdateWeaponImage(weaponSystem.CurrentWeapon);
        uiManager.UpdateBombText(weaponSystem);
    }



}
