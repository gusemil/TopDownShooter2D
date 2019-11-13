using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameManager : MonoBehaviour
{
    private PlayerStats playerStats;

    public static GameManager status; //miten tämä on singleton?
    public PlayerStats PlayerStats { get { return playerStats; } }
    
    //public float playerHealth;
    //public float playerMaxHealth;

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

        //playerHealth = 100;
        //playerMaxHealth = 100;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
    [Serializable]
    class PlayerData
    {
        public float health;
        public float maxHealth;
        public string currentLevel;
    }
    */
}
