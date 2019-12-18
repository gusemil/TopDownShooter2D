using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaves : MonoBehaviour
{
    public static float crabSpawnTimer = 0;
    public static float jumperSpawnTimer = 0;
    public static float octopusSpawnTimer = 0;
    private float crabSpawnRate = 0.5f; //0.5f
    private float jumperSpawnRate = 5f; //5f
    private float octopusSpawnRate = 3f; //3f

    private GameManager gameManager;

    public GameObject spawnNorth;
    public GameObject spawnEast;
    public GameObject spawnSouth;
    public GameObject spawnWest;

    public GameObject crab;
    public GameObject jumper;
    public GameObject octopus;

    public float CrabSpawnTimer { get { return crabSpawnTimer; } set { crabSpawnTimer = value; } }
    public float JumperSpawnTimer { get { return jumperSpawnTimer; } set { jumperSpawnTimer = value; } }
    public float OctopusSpawnTimer { get { return octopusSpawnTimer; } set { octopusSpawnTimer = value; } }


    // Start is called before the first frame update
    void Start()
    {
        //gameManager = GameManager;
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
                SpawnEnemy(crab,spawnNorth);
                crabSpawnTimer = 0;
            }

            if (jumperSpawnTimer >= jumperSpawnRate)
            {
                SpawnEnemy(jumper,spawnNorth);
                jumperSpawnTimer = 0;
            }

            if (octopusSpawnTimer >= octopusSpawnRate)
            {
                SpawnEnemy(octopus,spawnNorth);
                octopusSpawnTimer = 0;
            }

        //}
    }

    public void SpawnEnemy(GameObject enemyType, GameObject spawnPoint)
    {
        //Lista vihollisista myöhemmin
        Instantiate(enemyType, spawnPoint.transform.position, Quaternion.identity); //Quaternion.identity = no rotation
    }
}
