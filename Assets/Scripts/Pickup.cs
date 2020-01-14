using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public float powerUpDuration;

    private PlayerController pc;
    private bool isPowerUpOn = false;
    private bool pickupFlickerOn = false;
    private float pickupDestroyTime = 10f;
    private float pickupTimer = 0f;
    private bool isPickedUp = false;
    private float pickupFlickerStart;
    private float powerUpTimer;
    private string powerUpName;
    private UIManager uiManager;
    private AudioManager audioManager;

    private void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
        powerUpTimer = powerUpDuration;
        audioManager = AudioManager.instance;
        pickupFlickerStart = pickupDestroyTime - (pickupDestroyTime / 3);
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
        }
        else if (pickupTimer >= pickupFlickerStart && !pickupFlickerOn)
        {
            pickupFlickerOn = true;
            StartCoroutine(PickupFlicker());
        }


        if (isPowerUpOn)
        {
            powerUpTimer -= Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {

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
            else if (gameObject.tag == "PointMultiplier")
            {
                isPickedUp = true;
                PointMultiplierPack();
            }
            else if (gameObject.tag == "Shield" && !playerStats.IsShieldUp)
            {
                isPickedUp = true;
                ShieldPack(playerStats, other);
            }
            else if (gameObject.tag == "InfiniteAmmo" && !playerStats.IsInfiniteAmmoUp)
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

        for (int i = 0; i < weapon.WeaponList.Count; i++)
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
        audioManager.PlaySound(31, 1.5f);
        StartCoroutine(uiManager.ShowPowerUpText("SHIELD"));
        pc = playerCollider.GetComponent<PlayerController>();
        stats.IsShieldUp = true;
        pc.TurnOnShieldGraphic();
        RemoveGraphicsAndCollider();
        Destroy(gameObject, 3f);
    }

    private IEnumerator PowerUp(PlayerStats player, Collider2D playerCollider)
    {
        RemoveGraphicsAndCollider();
        isPowerUpOn = true;
        pc = playerCollider.GetComponent<PlayerController>();

        powerUpName = gameObject.tag;

        if (gameObject.tag == "HexDamage")
        {
            audioManager.PlaySound(9);
            audioManager.PlaySound(29, 1.5f);
            player.IsHexDamageUp = true;
            pc.TurnOnHexDamageEffect();
            StartCoroutine(uiManager.ShowPowerUpText("HEX DAMAGE"));
        }
        else if (gameObject.tag == "InfiniteAmmo")
        {
            audioManager.PlaySound(13);
            audioManager.PlaySound(32, 1.5f);
            player.IsInfiniteAmmoUp = true;
            StartCoroutine(uiManager.ShowPowerUpText("INFINITE AMMO"));
            pc.TurnOnInfiniteAmmoEffect();
        }
        else if (gameObject.tag == "InfiniteDash")
        {
            audioManager.PlaySound(10);
            audioManager.PlaySound(30, 1.5f);
            player.IsInfiniteDashUp = true;
            pc.TurnOnInfiniteDashEffect();
            StartCoroutine(uiManager.ShowPowerUpText("INFINITE DASH"));
        }
        else if (gameObject.tag == "GodMode")
        {
            audioManager.PlaySound(14);
            audioManager.PlaySound(33, 1.5f);
            player.IsGodModeUp = true;
            pc.TurnOnGodModeEffect();
            StartCoroutine(uiManager.ShowPowerUpText("GODLIKE"));
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
            }
            else if (gameObject.tag == "InfiniteAmmo")
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
            else if (gameObject.tag == "GodMode")
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
    }

    private void RemoveGraphicsAndCollider()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }

    private IEnumerator PickupFlicker()
    {
        while (!isPickedUp)
        {
            GetComponent<SpriteRenderer>().enabled = !GetComponent<SpriteRenderer>().enabled;
            yield return new WaitForSeconds(0.025f * pickupTimer);
        }
    }


}
