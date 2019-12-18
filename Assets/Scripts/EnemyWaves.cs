using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaves : MonoBehaviour
{
    public static float crabSpawnTimer = 0;
    public static float jumperSpawnTimer = 0;
    public static float octopusSpawnTimer = 0;
    private float crabSpawnRate = 1f; //0.5f
    private float jumperSpawnRate = 5f; //5f
    private float octopusSpawnRate = 3f; //3f

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
    }

    

    // Update is called once per frame
    void Update()
    {
        //if (gameManager.IsGameOver)
        //{
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

        //}
    }

    public void SpawnEnemy(GameObject enemyType)
    {
        //Lista vihollisista myöhemmin

        Instantiate(enemyType, spawnPoints[Random.Range(0,8)].transform.position, Quaternion.identity); //Quaternion.identity = no rotation
    }
}
