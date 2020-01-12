using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private Pickup pickup;
    public float powerUpDuration;

    private PlayerController pc;
    private bool isPowerUpOn = false;
    private bool pickupFlickerOn = false;
    private float pickupDestroyTime = 10f;
    private float pickupTimer = 0f;
    private bool isPickedUp = false;
    private float pickupFlickerStart;
    //private bool isFlickering = false;
    //private Color pickupColor;
    private float powerUpTimer;
    private string powerUpName;
    private UIManager uiManager;
    private AudioManager audioManager;

    //private List<Pickup> powerUpList = new List<Pickup>();

    //public List<Pickup> PowerUpList { get { return powerUpList; } set { powerUpList = value; } }

    //public float PowerUpTimer { get { return powerUpTimer; } }
    //public string PowerUpName { get { return powerUpName; } }

    private void Start()
    {
        //pickupColor = gameObject.GetComponent<SpriteRenderer>().color;
        uiManager = FindObjectOfType<UIManager>();
        powerUpTimer = powerUpDuration;
        audioManager = AudioManager.instance;
        pickupFlickerStart = pickupDestroyTime - (pickupDestroyTime / 3);
        //pickup = new Pickup();
    }

    private void Update()
    {

        if (!isPickedUp)
        {
            pickupTimer += Time.deltaTime;
        }

        if (pickupTimer >= pickupDestroyTime)
        {
            Destroy(gameObject);
        } else if (pickupTimer >= pickupFlickerStart && !pickupFlickerOn) {
            pickupFlickerOn = true;
            StartCoroutine(PickupFlicker());
        }
        

        if (isPowerUpOn) 
        {
            powerUpTimer -= Time.deltaTime;
           // uiManager.UpdatePowerUpText(powerUpTimer, powerUpName);
            //Debug.Log(powerupTimer);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //triggers even when colliding with an enemy

        if (other.tag == "Player")
        {
            PlayerStats playerStats = GameManager.instance.PlayerStats;
            WeaponSystem weapon = GameManager.instance.WeaponSystem;


            if (gameObject.tag == "HexDamage" && !playerStats.IsHexDamageUp)
            {
                isPickedUp = true;
                StartCoroutine(PowerUp(playerStats, other));
            }
            else if (gameObject.tag == "Ammo")
            {
                isPickedUp = true;
                AmmoPack(weapon);
            }
            else if (gameObject.tag == "Bomb")
            {
                isPickedUp = true;
                BombPack(weapon);
            }
            else if(gameObject.tag == "PointMultiplier")
            {
                isPickedUp = true;
                PointMultiplierPack();
            }
            else if(gameObject.tag == "Shield" && !playerStats.IsShieldUp)
            {
                isPickedUp = true;
                ShieldPack(playerStats, other);
            }
            else if(gameObject.tag == "InfiniteAmmo" && !playerStats.IsInfiniteAmmoUp)
            {
                isPickedUp = true;
                StartCoroutine(PowerUp(playerStats, other));
            }
            else if (gameObject.tag == "InfiniteDash" && !playerStats.IsInfiniteDashUp)
            {
                isPickedUp = true;
                StartCoroutine(PowerUp(playerStats, other));
            }
            else if (gameObject.tag == "GodMode" && !playerStats.IsGodModeUp)
            {
                isPickedUp = true;
                StartCoroutine(PowerUp(playerStats, other));
            }
        }
    }

    private void BombPack(WeaponSystem weapon)
    {
        audioManager.PlaySound(11);
        weapon.BombCount++;
        uiManager.UpdateBombText(weapon);
        Destroy(gameObject);
    }

    private void AmmoPack(WeaponSystem weapon)
    {
        GameManager gm = GameManager.instance;

        for(int i=0; i < weapon.WeaponList.Count; i++)
        {
            weapon.WeaponList[i].Ammo += weapon.WeaponList[i].AmmoFromPickup;

        }

        uiManager.UpdateWeaponText(weapon.CurrentWeapon);
        audioManager.PlaySound(7);

        Destroy(gameObject);
    }

    private void PointMultiplierPack()
    {
        audioManager.PlaySound(8);
        GameManager gm = GameManager.instance;
        gm.PointsMultiplier += 1;
        uiManager.UpdatePointMultiplierText(gm);
        Destroy(gameObject);
    }

    private void ShieldPack(PlayerStats stats, Collider2D playerCollider)
    {
        audioManager.PlaySound(12);
        pc = playerCollider.GetComponent<PlayerController>();
        stats.IsShieldUp = true;
        pc.TurnOnShieldGraphic();
        Destroy(gameObject);
    }

    private IEnumerator PowerUp(PlayerStats player, Collider2D playerCollider)
    {
        RemoveGraphicsAndCollider();
        isPowerUpOn = true;
        pc = playerCollider.GetComponent<PlayerController>();

        powerUpName = gameObject.tag;
       // powerUpList.Add(pickup);

        

        if (gameObject.tag == "HexDamage")
        {
            audioManager.PlaySound(9);
            player.IsHexDamageUp = true;
            pc.TurnOnHexDamageEffect();
            //pc.ChangePlayerColor(pc.HexDamageColor);
        } else if (gameObject.tag == "InfiniteAmmo")
        {
            audioManager.PlaySound(13);
            player.IsInfiniteAmmoUp = true;
            //pc.ChangePlayerColor(pc.InfiniteAmmoColor);
            pc.TurnOnInfiniteAmmoEffect();
        } else if (gameObject.tag == "InfiniteDash")
        {
            audioManager.PlaySound(10);
            player.IsInfiniteDashUp = true;
            pc.TurnOnInfiniteDashEffect();
            //pc.ChangePlayerColor(pc.InfiniteDashColor);
        } else if(gameObject.tag == "GodMode")
        {
            audioManager.PlaySound(14);
            player.IsGodModeUp = true;
            pc.TurnOnGodModeEffect();
            //pc.ChangePlayerColor(pc.GodModeColor);
        }



        while (isPowerUpOn)
        {
            if (gameObject.tag == "HexDamage" || gameObject.tag == "GodMode")
            {
                player.DamageMultiplier = 6;
            }

            if (gameObject.tag == "GodMode")
            {
                pc.MoveSpeed = 15f;
            }

            yield return new WaitForSeconds(powerUpDuration);

            if (gameObject.tag == "HexDamage")
            {
                if (!player.IsGodModeUp)
                {
                    player.DamageMultiplier = 1;
                }
                if (player.IsHexDamageUp)
                {
                    audioManager.PlaySound(15);
                }
                player.IsHexDamageUp = false;
                pc.TurnOffHexDamageEffect();
            } else if (gameObject.tag == "InfiniteAmmo")
            {
                if (player.IsInfiniteAmmoUp)
                {
                    audioManager.PlaySound(18);
                }
                player.IsInfiniteAmmoUp = false;
                pc.TurnOffInfiniteAmmoEffect();
            }
            else if (gameObject.tag == "InfiniteDash")
            {
                if (player.IsInfiniteDashUp)
                {
                    audioManager.PlaySound(16);          
                }
                pc.TurnOffInfiniteDashEffect();
                player.IsInfiniteDashUp = false;
            }
            else if(gameObject.tag == "GodMode")
            {
                if (!player.IsHexDamageUp)
                {
                    player.DamageMultiplier = 1;
                }
                pc.MoveSpeed = 7.5f;
                if (player.IsGodModeUp)
                {
                    audioManager.PlaySound(19);
                }
                pc.TurnOffGodModeEffect();
                player.IsGodModeUp = false;
                
            }

            isPowerUpOn = false;

            pc.ChangePlayerColor(pc.OriginalColor);
            Destroy(gameObject);
        }

       // powerUpList.Remove(pickup);
    }

    private void RemoveGraphicsAndCollider()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }

    /*
    private void DestroyObject()
    {
        Destroy(gameObject);
    }*/

        
    private IEnumerator PickupFlicker()
    {
        while (!isPickedUp)
        {
            GetComponent<SpriteRenderer>().enabled =! GetComponent<SpriteRenderer>().enabled;
            yield return new WaitForSeconds(0.025f * pickupTimer);
        }
    }
    

}
