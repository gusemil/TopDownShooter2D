using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject levelChangePanel;
    public GameObject buttonManager;
    public GameObject optionsPanel;
    public GameObject bloodCheck;

    public AudioMixer soundMixer;
    public AudioMixer musicMixer;

    // Start is called before the first frame update
    void Start()
    {
        levelChangePanel.SetActive(false);
        optionsPanel.SetActive(false);
        AudioManager.instance.PlayMusic(0);

        if (LevelManager.instance.IsBloodOn)
        {
            bloodCheck.GetComponent<Toggle>().isOn = true;
        } else
        {
            bloodCheck.GetComponent<Toggle>().isOn = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayLevel1()
    {
        LevelManager.instance.CurrentLevel = 1;
        SceneManager.LoadScene(1);
        AudioManager.instance.PlayMusic(1);
    }

    public void PlayLevel2()
    {
        LevelManager.instance.CurrentLevel = 2;
        SceneManager.LoadScene(1);
        AudioManager.instance.PlayMusic(2);
    }

    public void PlayLevel3()
    {
        LevelManager.instance.CurrentLevel = 3;
        SceneManager.LoadScene(1);
        AudioManager.instance.PlayMusic(3);
    }

    public void PlayEndlessMode()
    {
        LevelManager.instance.CurrentLevel = 4;
        SceneManager.LoadScene(1);
        AudioManager.instance.PlayMusic(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowLevelMenu()
    {
        AudioManager.instance.PlaySound(26);
        levelChangePanel.SetActive(true);
        buttonManager.SetActive(false);

    }

    public void ShowOptionsMenu()
    {
        AudioManager.instance.PlaySound(26);
        optionsPanel.SetActive(true);
        buttonManager.SetActive(false);
    }

    public void QuitLevelMenu()
    {
        AudioManager.instance.PlaySound(26);
        levelChangePanel.SetActive(false);
        buttonManager.SetActive(true);
    }

    public void QuitOptionsMenu()
    {
        AudioManager.instance.PlaySound(26);
        optionsPanel.SetActive(false);
        buttonManager.SetActive(true);
    }

    public void BloodToggle()
    {
        if (bloodCheck.GetComponent<Toggle>().isOn)
        {
            LevelManager.instance.IsBloodOn = true;
        } else
        {
            LevelManager.instance.IsBloodOn = false;
        }
    }
}


/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Options : MonoBehaviour
{
    public GameObject optionsPanel;
    public bool isActive;

    public AudioMixer mixer;

    // Start is called before the first frame update
    void Start()
    {
        optionsPanel.SetActive(false);
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            ShowOptions();
        }
    }

    public void ShowOptions()
    {
        if(!isActive)
        {
            optionsPanel.SetActive(true);
            isActive = true;
        } else
        {
            optionsPanel.SetActive(false);
            isActive = false;
        }
    }

    public void SetLevel(float sliderValue)
    {
        mixer.SetFloat("Volume", Mathf.Log10(sliderValue) * 20);
    }
}
*/