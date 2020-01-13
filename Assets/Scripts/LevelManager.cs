using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class LevelManager : MonoBehaviour
{

    public static LevelManager instance;

    private int currentLevel;
    private string currentSceneName;
    private bool isBloodOn;
    private int highestUnlockedLevel;

    public int CurrentLevel { get { return currentLevel; } set { currentLevel = value; } }
    public string CurrentSceneName { get { return currentSceneName; } set { currentSceneName = value; } }
    public bool IsBloodOn { get { return isBloodOn; } set { isBloodOn = value; } }
    public int HighestUnlockedLevel { get { return highestUnlockedLevel; } set { highestUnlockedLevel = value; } }

    [Serializable]
    class PlayerProgress
    {
        public int highestUnlockedLevel;
    }

    private void Awake()
    {
        if (instance == null) //jos status olio ei ole olemassa
        {
            DontDestroyOnLoad(gameObject);
            Debug.Log("creating levelmanager");
            instance = this;
            currentLevel = 0;
            currentSceneName = SceneManager.GetActiveScene().name;
            isBloodOn = true;
            //Load(instance);

        } else
        {
            Destroy(gameObject);
        }

        //DontDestroyOnLoad(gameObject);
    }

    public void Save(LevelManager lvlManager)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerProgress.dat");                //kerrotaan minne halutaan tallentaa tuleva data
        PlayerProgress progress = new PlayerProgress();
        progress.highestUnlockedLevel = highestUnlockedLevel;
        bf.Serialize(file, progress);
        file.Close();
    }

    public void Load(LevelManager lvlManager)
    {
        if (File.Exists(Application.persistentDataPath + "/playerProgress.dat")) //jos savedataa on olemassa niin annetaan loadaa, muuten tulee problems
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerProgress.dat", FileMode.Open);
            PlayerProgress progress = (PlayerProgress)bf.Deserialize(file);
            file.Close();
            highestUnlockedLevel = progress.highestUnlockedLevel;
        }
        else // if save file doesn't exist
        {
            Debug.Log("Creating save");
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/playerProgress.dat");                //kerrotaan minne halutaan tallentaa tuleva data
            PlayerProgress progress = new PlayerProgress();
            progress.highestUnlockedLevel = 1;
            bf.Serialize(file, progress);
            file.Close();
        }
    }
}
