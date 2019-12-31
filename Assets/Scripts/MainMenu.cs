using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject levelChangePanel;
    public GameObject buttonManager;

    // Start is called before the first frame update
    void Start()
    {
        levelChangePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayLevel1()
    {
        LevelManager.instance.CurrentLevel = 1;
        SceneManager.LoadScene(1);
    }

    public void PlayLevel2()
    {
        LevelManager.instance.CurrentLevel = 2;
        SceneManager.LoadScene(1);
    }

    public void PlayLevel3()
    {
        LevelManager.instance.CurrentLevel = 3;
        SceneManager.LoadScene(1);
    }

    public void PlayEndlessMode()
    {
        LevelManager.instance.CurrentLevel = 4;
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowLevelMenu()
    {
        levelChangePanel.SetActive(true);
        buttonManager.SetActive(false);

    }

    public void QuitLevelMenu()
    {
        levelChangePanel.SetActive(false);
        buttonManager.SetActive(true);
    }
}
