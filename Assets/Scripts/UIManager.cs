using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public Image life1;
    public Image life2;
    public Image life3;

    public Image dash1;
    public Image dash2;
    public Image dash3;
    public Image dash4;

    public Image gameOverScreen;
    public Text gameOverText;
    public Text pauseText;

    // Start is called before the first frame update
    void Start()
    {
        //weaponNameText.text = ws.CurrentWeapon.WeaponName;
        //ammoText.text = ws.CurrentWeapon.Ammo.ToString();

        uiManager = FindObjectOfType<UIManager>();

        gameOverScreen.enabled = false;
        gameOverText.enabled = false;
        pauseText.enabled = false;

        dash = GameManager.instance.Dash;
        currentDash = dash4;
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
            //dash4.fillAmount = 0f;
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

        /*
         * if (timerOn)
    {
        timer = timer - Time.deltaTime;
        Debug.Log("Timer" + timer);

        // Radial Fill
        radialImage.fillAmount = timer / startTime;

        if (timer <= 0)
        {
            timer = startTime;
            timerOn = false;
            radialImage.fillAmount = 0f;
        }
         * 
         */
    }

    private void DashUIUpdate(Image currentDash)
    {
        //dash timer stuff
        currentDash.fillAmount = dash.DashTimer / dash.DashRechargeTime;

        Debug.Log(dash.DashTimer / dash.DashRechargeTime);

        if (Dash.dashTimer <= 0)
        {
            currentDash.fillAmount = 1f;
        }
    }

    public void UpdateWeaponText(Weapon currentweapon)
    {
        weaponNameText.text = currentweapon.WeaponName;
        ammoText.text = currentweapon.Ammo.ToString();
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

    public void UpdateDashes(Dash dash)
    {
        
        if(dash.Dashes == 3)
        {
            dash4.fillAmount = 0f;

            dash4.enabled = true;
            dash3.enabled = true;
            dash2.enabled = true;
            dash1.enabled = true;
        }
        else if (dash.Dashes == 2)
        {
            dash3.fillAmount = 0f;

            dash4.enabled = false;
            dash3.enabled = true;
            dash2.enabled = true;
            dash1.enabled = true;
        }
        else if (dash.Dashes == 1)
        {
            dash2.fillAmount = 0f;

            dash4.enabled = false;
            dash3.enabled = false;
            dash2.enabled = true;
            dash1.enabled = true;
        }
        else if (dash.Dashes <= 0)
        {
            dash1.fillAmount = 0f;

            dash4.enabled = false;
            dash3.enabled = false;
            dash2.enabled = false;
            dash1.enabled = true;
        } else
        {
            dash4.enabled = true;
            dash3.enabled = true;
            dash2.enabled = true;
            dash1.enabled = true;
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
