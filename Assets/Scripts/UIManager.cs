using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //private GameManager gm;
    //private WeaponSystem ws;
    private UIManager uiManager;

    //UI
    public Text weaponNameText;
    public Text ammoText;
    //public Text pointsMultiplierText;
    public Text scoreText;

    public Image life1;
    public Image life2;
    public Image life3;
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateWeaponText(Weapon currentweapon)
    {
        weaponNameText.text = currentweapon.WeaponName;
        ammoText.text = currentweapon.Ammo.ToString();
    }

    /*
    public void UpdatePointMultiplierText(GameManager gm)
    {
        pointsMultiplierText.text = gm.PointsMultiplier.ToString() + "X";
    }
    */

    public void UpdateScore(GameManager gm)
    {
        scoreText.text = gm.Points.ToString() + " " + gm.PointsMultiplier.ToString() + "X";
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
