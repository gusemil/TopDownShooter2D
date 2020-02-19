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
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
            currentLevel = 0;
            currentSceneName = SceneManager.GetActiveScene().name;
            isBloodOn = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Save(LevelManager lvlManager, int levelToUnlock)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = File.Create(Application.persistentDataPath + "/playerProgress.dat");
        PlayerProgress progress = new PlayerProgress();
        progress.highestUnlockedLevel = levelToUnlock;
        bf.Serialize(fs, progress);
        fs.Close();
    }

    public void Load(LevelManager lvlManager)
    {
        if (File.Exists(Application.persistentDataPath + "/playerProgress.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerProgress.dat", FileMode.Open);
            PlayerProgress progress = (PlayerProgress)bf.Deserialize(file);
            highestUnlockedLevel = progress.highestUnlockedLevel;
            file.Close();
        }
        else // if save file doesn't exist, create a save
        {
            Save(lvlManager, 1);
        }
    }
}
