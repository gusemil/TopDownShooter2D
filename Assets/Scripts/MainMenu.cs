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
    public GameObject level4button;


    private void OnGUI()
    {
        GUI.Label(new Rect(200, 10, 200, 20), "Highest level unlocked: " + lvlManager.HighestUnlockedLevel);
    }

    private void Awake()
    {
        //saveManager = saveManager.Instance;
        lvlManager = FindObjectOfType<LevelManager>();
        //soundSliderValue = 1;
        //musicSliderValue = 1;
    }
    // Start is called before the first frame update
    void Start()
    {
        levelChangePanel.SetActive(false);
        optionsPanel.SetActive(false);
        AudioManager.instance.PlayMusic(0,0.75f);

        if (lvlManager.IsBloodOn)
        {
            bloodCheck.GetComponent<Toggle>().isOn = true;
        } else
        {
            bloodCheck.GetComponent<Toggle>().isOn = false;
        }

        level2button.SetActive(false);
        level3button.SetActive(false);
        level4button.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayLevel1()
    {
        LevelManager.instance.CurrentLevel = 1;
        SceneManager.LoadScene(1);
        AudioManager.instance.PlayMusic(1,1.5f);
    }

    public void PlayLevel2()
    {
        LevelManager.instance.CurrentLevel = 2;
        SceneManager.LoadScene(1);
        AudioManager.instance.PlayMusic(2,1f);
    }

    public void PlayLevel3()
    {
        LevelManager.instance.CurrentLevel = 3;
        SceneManager.LoadScene(1);
        AudioManager.instance.PlayMusic(3,1f);
    }

    public void PlayEndlessMode()
    {
        LevelManager.instance.CurrentLevel = 4;
        SceneManager.LoadScene(1);
        AudioManager.instance.PlayMusic(1,1f);
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
        Debug.Log("Highest unlocked level" + lvlManager.HighestUnlockedLevel);

        if (lvlManager.HighestUnlockedLevel >= 2)
        {
            level2button.SetActive(true);
        }

        if (lvlManager.HighestUnlockedLevel >= 3)
        {
            level3button.SetActive(true);
        }

        if(lvlManager.HighestUnlockedLevel == 4)
        {
            level4button.SetActive(true);
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

    public void ResetSave()
    {
        AudioManager.instance.PlaySound(16);
        lvlManager.HighestUnlockedLevel = 1;
        lvlManager.Save(lvlManager);
        lvlManager.Load(lvlManager);
    }

    public void BloodToggle()
    {
        if (bloodCheck.GetComponent<Toggle>().isOn)
        {
            lvlManager.IsBloodOn = true;
        } else
        {
            lvlManager.IsBloodOn = false;
        }
    }

    public void SetSoundLevel(float sliderValue)
    {
        soundMixer.SetFloat("SoundVolume", Mathf.Log10(sliderValue) * 20);
        //soundSliderValue = sliderValue;
        AudioManager.instance.SoundSliderValue = soundSlider.GetComponent<Slider>().value;
    }

    public void SetMusicLevel(float sliderValue)
    {
        musicMixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
        //musicSliderValue = sliderValue;
        AudioManager.instance.MusicSliderValue = musicSlider.GetComponent<Slider>().value;
    }

    public void PlaySoundSliderSound()
    {
        AudioManager.instance.PlaySound(26);
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
