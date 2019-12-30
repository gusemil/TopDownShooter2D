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
    public Text pauseText;

    public Text waveText;

    public Text powerUpText1;
    public Text powerUpText2;
    public Text powerUpText3;
    public Text powerUpText4;

    private float waveTextDuration = 3f;

    // Start is called before the first frame update
    void Start()
    {
        //weaponNameText.text = ws.CurrentWeapon.WeaponName;
        //ammoText.text = ws.CurrentWeapon.Ammo.ToString();

        uiManager = FindObjectOfType<UIManager>();

        gameOverScreen.enabled = false;
        gameOverText.enabled = false;
        gameOverPointsText.enabled = false;
        gameOverPointsAmount.enabled = false;
        restartText.enabled = false;
        pauseText.enabled = false;
        waveText.enabled = false;

        powerUpText1.enabled = true;
        powerUpText2.enabled = false;
        powerUpText3.enabled = false;
        powerUpText4.enabled = false;

        dash = GameManager.instance.Dash;
        currentDash = dash4;
        weaponIcon.sprite = pistolSprite;

        StartCoroutine(ShowWaveText(GameManager.instance.EnemyWaves));
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

    public void UpdatePowerUpText(float timer, string powerUpName)
    {
        //Math.Round(timer,2);
       // if(powerUpText1.enabled)
        //{
            powerUpText1.text = Math.Round(timer).ToString() + " " + powerUpName;
       // }
        /*
        else if(powerUpText1.enabled && !powerUpText2.enabled)
        {
            powerUpText2.enabled = true;
            powerUpText2.text = Math.Round(timer).ToString() + " " + powerUpName;
        }
        */
    }

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
        gameOverText.enabled = true;
        gameOverScreen.enabled = true;
        restartText.enabled = true;
        gameOverPointsText.enabled = true;
        gameOverPointsAmount.enabled = true;

        gameOverPointsAmount.text = GameManager.instance.Points.ToString();
    }

    public IEnumerator ShowWaveText(EnemyWaves ew)
    {
            waveText.enabled = true;
            waveText.text = "Wave " + ew.Wave;
            yield return new WaitForSeconds(waveTextDuration);
            waveText.enabled = false;
    }


    public void TogglePauseText(Pause pause)
    {
        if (pause.IsPause)
        {
            if (!gameOverText.enabled)
            {
                pauseText.enabled = true;
            }
        } else
        {
            pauseText.enabled = false;
        }
    }
}
