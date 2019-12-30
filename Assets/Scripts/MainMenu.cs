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

    public void PlayEndlessMode()
    {
        SceneManager.LoadScene("SampleScene");
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
