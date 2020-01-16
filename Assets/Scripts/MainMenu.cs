using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private LevelManager lvlManager;

    public GameObject levelChangePanel;
    public GameObject buttonManager;
    public GameObject optionsPanel;
    public GameObject bloodCheck;
    public AudioMixer soundMixer;
    public AudioMixer musicMixer;
    public GameObject soundSlider;
    public GameObject musicSlider;
    public GameObject level2button;
    public GameObject level3button;

    private void Awake()
    {
        lvlManager = FindObjectOfType<LevelManager>();
    }
    void Start()
    {
        levelChangePanel.SetActive(false);
        optionsPanel.SetActive(false);
        AudioManager.instance.PlayMusic(0, 0.5f);

        if (lvlManager.IsBloodOn)
        {
            bloodCheck.GetComponent<Toggle>().isOn = true;
        }
        else
        {
            bloodCheck.GetComponent<Toggle>().isOn = false;
        }
        level2button.SetActive(false);
        level3button.SetActive(false);
    }

    public void PlayLevel(int level)
    {
        LevelManager.instance.CurrentLevel = level;
        SceneManager.LoadScene(1);
        if(level == 1 || level == 4)
        {
            AudioManager.instance.PlayMusic(1, 1.5f);
        }
        else
        {
            AudioManager.instance.PlayMusic(level, 1f);
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowLevelMenu()
    {
        AudioManager.instance.PlaySound(26);
        lvlManager.Load(lvlManager);
        levelChangePanel.SetActive(true);
        buttonManager.SetActive(false);

        if (lvlManager.HighestUnlockedLevel >= 2)
        {
            level2button.SetActive(true);
        }
        else
        {
            level2button.SetActive(false);
        }

        if (lvlManager.HighestUnlockedLevel >= 3)
        {
            level3button.SetActive(true);
        }
        else
        {
            level3button.SetActive(false);
        }

    }

    public void ShowOptionsMenu()
    {
        AudioManager.instance.PlaySound(26);
        optionsPanel.SetActive(true);
        buttonManager.SetActive(false);

        soundSlider.GetComponent<Slider>().value = AudioManager.instance.SoundSliderValue;
        musicSlider.GetComponent<Slider>().value = AudioManager.instance.MusicSliderValue;
    }

    public void BackToMainMenu()
    {
        AudioManager.instance.PlaySound(26);
        if (levelChangePanel.activeSelf)
        {
            levelChangePanel.SetActive(false);
        } else if (optionsPanel.activeSelf)
        {
            optionsPanel.SetActive(false);
        }
        buttonManager.SetActive(true);
    }

    public void ResetSave()
    {
        AudioManager.instance.PlaySound(16);
        lvlManager.HighestUnlockedLevel = 1;
        lvlManager.Save(lvlManager);
        lvlManager.Load(lvlManager);
    }

    public void UnlockAllMaps()
    {
        AudioManager.instance.PlaySound(12);
        lvlManager.HighestUnlockedLevel = 3;
        lvlManager.Save(lvlManager);
        lvlManager.Load(lvlManager);
    }

    public void BloodToggle()
    {
        if (bloodCheck.GetComponent<Toggle>().isOn)
        {
            lvlManager.IsBloodOn = true;
        }
        else
        {
            lvlManager.IsBloodOn = false;
        }
    }

    public void SetSoundLevel(float sliderValue)
    {
        soundMixer.SetFloat("SoundVolume", Mathf.Log10(sliderValue) * 20);
        AudioManager.instance.SoundSliderValue = soundSlider.GetComponent<Slider>().value;
    }

    public void SetMusicLevel(float sliderValue)
    {
        musicMixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
        AudioManager.instance.MusicSliderValue = musicSlider.GetComponent<Slider>().value;
    }

    public void PlaySoundSliderSound()
    {
        AudioManager.instance.PlaySound(26);
    }
}
