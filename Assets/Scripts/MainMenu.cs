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
        SceneChangeManager.instance.CurrentLevel = 1;
        SceneManager.LoadScene(1);
    }

    public void PlayLevel2()
    {
        SceneChangeManager.instance.CurrentLevel = 2;
        SceneManager.LoadScene(1);
    }

    public void PlayLevel3()
    {
        SceneChangeManager.instance.CurrentLevel = 3;
        SceneManager.LoadScene(1);
    }

    public void PlayEndlessMode()
    {
        SceneChangeManager.instance.CurrentLevel = 4;
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
