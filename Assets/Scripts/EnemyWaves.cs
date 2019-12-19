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

    private int enemiesSpawned;
    private int enemiesPerWave;
    private int wave;
    private float waveSpawnDelay = -5f;

    private GameManager gameManager;

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
        enemiesPerWave = 50;
        enemiesAlive = 0;
        isSpawningPaused = false;
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(20, 270, 200, 40), "Enemies Spawned: " + enemiesSpawned);
        GUI.Label(new Rect(20, 290, 200, 40), "Wave Count: " + wave);
        GUI.Label(new Rect(20, 310, 200, 40), "CrabSpawnTimer: " + crabSpawnTimer);
        GUI.Label(new Rect(20, 330, 200, 40), "OctopusSpawnTimer: " + octopusSpawnTimer);
        GUI.Label(new Rect(20, 350, 200, 40), "JumperSpawnTimer: " + jumperSpawnTimer);
        GUI.Label(new Rect(20, 370, 200, 40), "Enemies Alive: " + enemiesAlive);
    }



    // Update is called once per frame
    void Update()
    {
        //if (gameManager.IsGameOver)
        //{

        if (isSpawningPaused && enemiesAlive == 0 && enemiesSpawned == 0)
        {
            NextWave();
        }

        if (!isSpawningPaused)
        {
            crabSpawnTimer += Time.deltaTime;
            jumperSpawnTimer += Time.deltaTime;
            octopusSpawnTimer += Time.deltaTime;
        }

            if (crabSpawnTimer >= crabSpawnRate)
            {
                crabSpawnTimer = 0;
                SpawnEnemy(crab);
            }

            if (jumperSpawnTimer >= jumperSpawnRate)
            {
                jumperSpawnTimer = 0;
                SpawnEnemy(jumper);
            }   

            if (octopusSpawnTimer >= octopusSpawnRate)
            {
                octopusSpawnTimer = 0;
                SpawnEnemy(octopus);
            }
    }

    public void SpawnEnemy(GameObject enemyType)
    {
        //Lista vihollisista myöhemmin

        Instantiate(enemyType, spawnPoints[Random.Range(0,8)].transform.position, Quaternion.identity); //Quaternion.identity = no rotation
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
            wave++;
            crabSpawnTimer = waveSpawnDelay;
            jumperSpawnTimer = waveSpawnDelay;
            octopusSpawnTimer = waveSpawnDelay;
            isSpawningPaused = false;

        crabSpawnRate -= 0.05f;
        jumperSpawnRate -= 0.1f;
        octopusSpawnRate -= 0.1f;
    }
    

    public void StopSpawning()
    {
        isSpawningPaused = true;
        enemiesSpawned = 0;
    }

}
