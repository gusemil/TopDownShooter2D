using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public static LevelManager instance;

    private int currentLevel;
    private string currentSceneName;
    private bool isBloodOn;

    public int CurrentLevel { get { return currentLevel; } set { currentLevel = value; } }
    public string CurrentSceneName { get { return currentSceneName; } set { currentSceneName = value; } }
    public bool IsBloodOn { get { return isBloodOn; } set { isBloodOn = value; } }


    private void Awake()
    {
        if (instance == null) //jos status olio ei ole olemassa
        {
            DontDestroyOnLoad(gameObject);
            Debug.Log("creating sceneManager");
            instance = this;
            currentLevel = 0;
            currentSceneName = SceneManager.GetActiveScene().name;
            isBloodOn = true;
        } else
        {
            Destroy(gameObject);
        }

        //DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
