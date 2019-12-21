using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public float powerUpDuration;
    private bool isPowerUpOn;
    PlayerController pc;

    private void Awake()
    {
        isPowerUpOn = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collided with powerup");

        if (other.transform.GetComponent<PlayerController>())
        {
            PlayerStats playerStats = GameManager.instance.PlayerStats;
            WeaponSystem weapon = GameManager.instance.WeaponSystem;

            if (gameObject.tag == "HexDamage" && !playerStats.IsHexDamageUp)
            {
                StartCoroutine(PowerUp(playerStats, other));
            }
            else if (gameObject.tag == "Ammo")
            {
                AmmoPack(weapon);
            }
            else if (gameObject.tag == "Bomb")
            {
                BombPack(weapon);
            }
            else if(gameObject.tag == "InfiniteAmmo" && !playerStats.IsInfiniteAmmoUp)
            {
                StartCoroutine(PowerUp(playerStats, other));
            }
        }
    }

    private void BombPack(WeaponSystem weapon)
    {
        weapon.BombCount++;
        Destroy(gameObject);
    }

    private void AmmoPack(WeaponSystem weapon)
    {
        weapon.WeaponList[1].Ammo += 1000;
        weapon.WeaponList[2].Ammo += 50;
        weapon.WeaponList[3].Ammo += 5;
        weapon.WeaponList[4].Ammo += 100;

        Destroy(gameObject);
    }

    private IEnumerator PowerUp(PlayerStats player, Collider2D playerCollider)
    {
        RemoveGraphicsAndCollider();
        isPowerUpOn = true;
        pc = playerCollider.GetComponent<PlayerController>();

        if (gameObject.tag == "HexDamage")
        {
            player.IsHexDamageUp = true;
            pc.ChangePlayerColor(pc.HexDamageColor);
        } else if (gameObject.tag == "InfiniteAmmo")
        {
            player.IsInfiniteAmmoUp = true;
            pc.ChangePlayerColor(pc.InfiniteAmmoColor);
        }

        while (isPowerUpOn)
        {
            //pc.ChangePlayerColor(pc.HexDamageColor);
            int originalDamageMultiplier = player.DamageMultiplier;
            player.DamageMultiplier = 6;
            yield return new WaitForSeconds(powerUpDuration);

            player.DamageMultiplier = originalDamageMultiplier;
            isPowerUpOn = false;

            if (gameObject.tag == "HexDamage")
            {
                player.IsHexDamageUp = false;
            } else if (gameObject.tag == "InfiniteAmmo")
            {
                player.IsInfiniteAmmoUp = false;
            }

            pc.ChangePlayerColor(pc.OriginalColor);
            Destroy(gameObject);
        }

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

}
