using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIManager : MonoBehaviour
{
    //private GameManager gm;
    //private WeaponSystem ws;
    private UIManager uiManager;
    private Dash dash;
    private Image currentDash;

    //UI
    public Text weaponNameText;
    public Text ammoText;
    public Text pointsMultiplierText;
    public Text scoreText;

    public Image bomb;
    public Text bombText;

    public Image life1;
    public Image life2;
    public Image life3;

    public Image dash1;
    public Image dash2;
    public Image dash3;
    public Image dash4;

    public Image weaponIcon;

    public Sprite pistolSprite;
    public Sprite machinegunSprite;
    public Sprite shotgunSprite;
    public Sprite rocketlauncherSprite;
    public Sprite flamethrowerSprite;

    public Image gameOverScreen;
    public Text gameOverText;
    public Text gameOverPointsText;
    public Text gameOverPointsAmount;
    public Text restartText;

    public GameObject pauseMenu;
    //public Text pauseText;

    public Text waveText;
    public Text PowerUpText;

    private float waveTextDuration = 3f;
    private float powerUpTextDuration = 2f;
    private float powerUpTextTimer;
    private LevelManager lvlManager;

    // Start is called before the first frame update
    void Start()
    {
        //weaponNameText.text = ws.CurrentWeapon.WeaponName;
        //ammoText.text = ws.CurrentWeapon.Ammo.ToString();

        uiManager = FindObjectOfType<UIManager>();
        lvlManager = FindObjectOfType<LevelManager>();

        gameOverScreen.enabled = false;
        gameOverText.enabled = false;
        gameOverPointsText.enabled = false;
        gameOverPointsAmount.enabled = false;
        restartText.enabled = false;
        waveText.enabled = false;
        PowerUpText.enabled = false;
        powerUpTextTimer = powerUpTextDuration;

        /*
        powerUpText1.enabled = true;
        powerUpText2.enabled = false;
        powerUpText3.enabled = false;
        powerUpText4.enabled = false;
        */

        pauseMenu.SetActive(false);
        //pauseText.enabled = false;

        dash = GameManager.instance.Dash;
        currentDash = dash4;
        //weaponIcon.sprite = pistolSprite;

        GameManager.instance.WeaponSystem.ChangeWeapon(0);
        uiManager.UpdateWeaponText(GameManager.instance.WeaponSystem.CurrentWeapon);
        uiManager.UpdateWeaponImage(GameManager.instance.WeaponSystem.CurrentWeapon);

        StartCoroutine(ShowWaveText(GameManager.instance.EnemyWaves));


        if (lvlManager.CurrentLevel == 1) //tutorial
        {
            GameManager.instance.Pause.TogglePause();
            uiManager.TogglePauseText(GameManager.instance.Pause);
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*
        //dash timer stuff
        dash4.fillAmount = dash.DashTimer / dash.DashRechargeTime;

       Debug.Log(dash.DashTimer / dash.DashRechargeTime);

        if (Dash.dashTimer <= 0)
        {
            dash4.fillAmount = 1f;
        }
        */

        if (dash.Dashes == 3)
        {
            dash4.fillAmount = 0f;
            currentDash = dash4;
        } else if (dash.Dashes == 2)
        {
            dash4.fillAmount = 0f;
            currentDash = dash3;
        } else if(dash.Dashes == 1)
        {
            dash3.fillAmount = 0f;
            currentDash = dash2;
        } else if (dash.Dashes == 0)
        {
            dash2.fillAmount = 0f;

            currentDash = dash1;
        }

        if (PowerUpText.enabled)
        {
            powerUpTextTimer -= Time.deltaTime;
        }

        DashUIUpdate(currentDash);

    }

    private void DashUIUpdate(Image currentDash)
    {
        //dash timer stuff
        currentDash.fillAmount = dash.DashTimer / dash.DashRechargeTime;

        //Debug.Log(dash.DashTimer / dash.DashRechargeTime);

        //Debug.Log(dash.DashTimer);

        if ( (dash.DashTimer <= 0) || dash.DashTimer >= dash.DashRechargeTime - 0.02f)
        {
            currentDash.fillAmount = 1f;
        }
    }

    /*
    public void UpdatePowerUpText(float timer, string powerUpName)
    {
        //Math.Round(timer,2);
       // if(powerUpText1.enabled)
        //{
           // powerUpText1.text = Math.Round(timer).ToString() + " " + powerUpName;
       // }
        /*
        else if(powerUpText1.enabled && !powerUpText2.enabled)
        {
            powerUpText2.enabled = true;
            powerUpText2.text = Math.Round(timer).ToString() + " " + powerUpName;
        }
        */
    //}

    public void UpdateWeaponText(Weapon currentweapon)
    {
        weaponNameText.text = currentweapon.WeaponName;
        ammoText.text = currentweapon.Ammo.ToString();
    }

    public void UpdateWeaponImage(Weapon currentweapon)
    {
        if(currentweapon.WeaponName == "Pistol")
        {
            weaponIcon.sprite = pistolSprite;
        } else if (currentweapon.WeaponName == "Machine Gun")
        {
            weaponIcon.sprite = machinegunSprite;
        } else if (currentweapon.WeaponName == "Shotgun")
        {
            weaponIcon.sprite = shotgunSprite;
        } else if (currentweapon.WeaponName == "Rocket Launcher")
        {
            weaponIcon.sprite = rocketlauncherSprite;
        } else if (currentweapon.WeaponName == "Flamethrower")
        {
            weaponIcon.sprite = flamethrowerSprite;
        }
    }

    public void UpdateBombText(WeaponSystem ws)
    {
        bombText.text = "x" + ws.BombCount.ToString();
    }

    
    public void UpdatePointMultiplierText(GameManager gm)
    {
        pointsMultiplierText.text = "X" + gm.PointsMultiplier.ToString();
    }

    public void UpdateScore(GameManager gm)
    {
        scoreText.text = gm.Points.ToString();
    }

    public void UpdateLives(GameManager gm)
    {
        if (gm.Lives == 2)
        {
            life3.enabled = false;
        }
        else if (gm.Lives == 1)
        {
            life2.enabled = false;
        }
        else if (gm.Lives <= 0)
        {
            life1.enabled = false;
        }
    }


    public void ShowGameOverScreen()
    {
        /*
        if (!gameOverScreen.enabled)
        {
            gameOverScreen.enabled = true;
        }
        else
        {
            gameOverScreen.enabled = false;
        }*/
        gameOverScreen.enabled = true;
        gameOverText.enabled = true;
        restartText.enabled = true;
        gameOverPointsText.enabled = true;
        gameOverPointsAmount.enabled = true;

        if (GameManager.instance.LevelComplete)
        {
            gameOverText.text = "LEVEL COMPLETE";
        }

        gameOverPointsAmount.text = GameManager.instance.Points.ToString();
    }

    public IEnumerator ShowWaveText(EnemyWaves ew)
    {
            waveText.enabled = true;

        if(LevelManager.instance.CurrentLevel != 4)
        {
            waveText.text = "Wave " + ew.Wave + "/" + ew.MaxWaves;
        } else
        {
            waveText.text = "Wave " + ew.Wave;
        }
            
            yield return new WaitForSeconds(waveTextDuration);
            waveText.enabled = false;
    }

    public IEnumerator ShowPowerUpText(String powerUpName)
    {
        PowerUpText.enabled = true;

        PowerUpText.text = powerUpName;

        yield return new WaitForSeconds(powerUpTextDuration);
        PowerUpText.enabled = false;
        

        /*
        while(powerUpTextTimer >= 0)
        {
            PowerUpText.enabled = true;
            PowerUpText.text = powerUpName;
        }
        */

    }



    public IEnumerator ShowLevelCompleteText(EnemyWaves ew)
    {
        waveText.enabled = true;
        waveText.text = "Level Complete!";
        yield return new WaitForSeconds(waveTextDuration);
        waveText.enabled = false;
    }


    public void TogglePauseText(Pause pause)
    {
        if (pause.IsPause)
        {
            if (!gameOverText.enabled)
            {
                pauseMenu.SetActive(true);
            }
        } else
        {
            pauseMenu.SetActive(false);
        }
    }
}
