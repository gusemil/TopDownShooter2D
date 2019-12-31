using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour
{

    public static SceneChangeManager instance;

    private int currentLevel;
    private string currentSceneName;

    public int CurrentLevel { get { return currentLevel; } set { currentLevel = value; } }
    public string CurrentSceneName { get { return currentSceneName; } set { currentSceneName = value; } }


    private void Awake()
    {
        if (instance == null) //jos status olio ei ole olemassa
        {
            DontDestroyOnLoad(gameObject);
            Debug.Log("creating sceneManager");
            instance = this;
            currentSceneName = SceneManager.GetActiveScene().name;
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
