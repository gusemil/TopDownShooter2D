using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaves : MonoBehaviour
{
    private static float crabSpawnTimer = 0;
    private static float jumperSpawnTimer = 0;
    private static float octopusSpawnTimer = 0;
    private float crabSpawnRate = 1f; //0.5f
    private float jumperSpawnRate = 5f; //5f
    private float octopusSpawnRate = 3f; //3f
    private static bool isSpawningPaused;
    private static int enemiesAlive;

    private static int enemiesSpawned;
    private int enemiesPerWave;
    private static int wave;
    private float waveSpawnDelay = -5f;
    private static int maxWaves;

    //private GameManager gameManager;
    private UIManager uiManager;
    private LevelManager lvlManager;

    public GameObject spawnNorth;
    public GameObject spawnEast;
    public GameObject spawnSouth;
    public GameObject spawnWest;
    public GameObject spawnNorthEast;
    public GameObject spawnSouthEast;
    public GameObject spawnSouthWest;
    public GameObject spawnNorthWest;

    public GameObject crab;
    public GameObject jumper;
    public GameObject octopus;

    public float CrabSpawnTimer { get { return crabSpawnTimer; } set { crabSpawnTimer = value; } }
    public float JumperSpawnTimer { get { return jumperSpawnTimer; } set { jumperSpawnTimer = value; } }
    public float OctopusSpawnTimer { get { return octopusSpawnTimer; } set { octopusSpawnTimer = value; } }
    public bool IsSpawningPaused { get { return isSpawningPaused; } }
    public int EnemiesAlive { get { return enemiesAlive; } set { enemiesAlive = value; } }
    public int EnemiesSpawned { get { return enemiesSpawned; } set { enemiesSpawned = value; } }
    public int Wave { get { return wave; } set { wave = value; } }
    public int MaxWaves { get { return maxWaves; } }

    private List<GameObject> spawnPoints = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        spawnPoints.Add(spawnNorth);
        spawnPoints.Add(spawnEast);
        spawnPoints.Add(spawnSouth);
        spawnPoints.Add(spawnWest);
        spawnPoints.Add(spawnNorthEast);
        spawnPoints.Add(spawnSouthEast);
        spawnPoints.Add(spawnSouthWest);
        spawnPoints.Add(spawnNorthWest);

        wave = 1;
        enemiesSpawned = 0;
        enemiesPerWave = 20;
        enemiesAlive = 0;
        isSpawningPaused = false;
        //gameManager = GameManager.instance;
        uiManager = FindObjectOfType<UIManager>();
        lvlManager = LevelManager.instance;

        if(lvlManager.CurrentLevel == 1)
        {
            maxWaves = 3;
            //GameManager.instance.Pause.TogglePause();
            //uiManager.TogglePauseText(GameManager.instance.Pause);
            //uiManager.pauseMenu.SetActive(true);
            /*
            pause.TogglePause();
            uiManager.TogglePauseText(pause);
            */
        }
        else if(lvlManager.CurrentLevel == 2)
        {
            maxWaves = 5;
        } else if(lvlManager.CurrentLevel == 3)
        {
            maxWaves = 7;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (gameManager.IsGameOver)
        //{

        if ( isSpawningPaused && enemiesAlive == 0 && enemiesSpawned == 0)
        {
            NextWave();
        }

        if (!isSpawningPaused)
        {
            crabSpawnTimer += Time.deltaTime;

            if(lvlManager.CurrentLevel >= 2)
            {
                jumperSpawnTimer += Time.deltaTime;
            }

            //jumperSpawnTimer += Time.deltaTime;

            if (lvlManager.CurrentLevel >= 3)
            {
                octopusSpawnTimer += Time.deltaTime;
            }
        }

            if (crabSpawnTimer >= crabSpawnRate)
            {
                crabSpawnTimer = 0;
                SpawnEnemy(crab);
            }

            if (jumperSpawnTimer >= jumperSpawnRate && lvlManager.CurrentLevel >= 2)
            {
                jumperSpawnTimer = 0;
                SpawnEnemy(jumper);
            }   

            if (octopusSpawnTimer >= octopusSpawnRate && lvlManager.CurrentLevel >= 3)
            {
                octopusSpawnTimer = 0;
                SpawnEnemy(octopus);
            }

            
        if (Input.GetKeyUp(KeyCode.N))
        {
            NextWave();
        }
        
    }

    public void SpawnEnemy(GameObject enemyType)
    {
        //Lista vihollisista myöhemmin

        Instantiate(enemyType, spawnPoints[Random.Range(0,spawnPoints.Count)].transform.position, Quaternion.identity); //Quaternion.identity = no rotation
        enemiesSpawned++;
        enemiesAlive++;

        if(enemiesSpawned >= enemiesPerWave * wave)
        {
            StopSpawning();
        }
        /*else
        {
            isSpawningPaused = false;
            //ContinueSpawning();
        }*/

    }

    
    public void NextWave()
    {
        isSpawningPaused = false;
        wave++;
        AudioManager.instance.PlaySound(24);
        
            crabSpawnTimer = waveSpawnDelay;
            jumperSpawnTimer = waveSpawnDelay;
            octopusSpawnTimer = waveSpawnDelay;

        if (crabSpawnRate > 0.1f)
            crabSpawnRate -= 0.1f;

        if(jumperSpawnRate > 1f)
            jumperSpawnRate -= 0.5f;

        if(octopusSpawnRate > 0.5f)
            octopusSpawnRate -= 0.25f;

        if(lvlManager.CurrentLevel != 4 && wave > maxWaves)
        {
            StartCoroutine(GameManager.instance.CompleteLevel());
        } else
        {
            StartCoroutine(uiManager.ShowWaveText(GameManager.instance.EnemyWaves));
        }

        if(lvlManager.CurrentLevel == 4)
        {
            if(wave == 4)
            {
                AudioManager.instance.PlayMusic(2,1f);
            } else if(wave == 8)
            {
                AudioManager.instance.PlayMusic(3,1f);
            }
        }
    }
    

    public void StopSpawning()
    {
        isSpawningPaused = true;
        enemiesSpawned = 0;
    }

}
