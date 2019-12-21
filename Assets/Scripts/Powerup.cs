using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    /*

    private float hexDamageDuration = 5f;
    private bool isHexDamageOn;
    private int healAmount = 20;
    

    private void Awake()
    {
        isHexDamageOn = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collided with powerup");

        if (other.transform.GetComponent<PlayerController>())
        {
            PlayerStats playerStats = GameManager.instance.PlayerStats;
            WeaponSystem weapon = GameManager.instance.WeaponSystem;
            if (gameObject.tag == "HpPack")
            {
                HpPack(playerStats);
            }
            else if (gameObject.tag == "HexDamage" && !playerStats.IsHexDamageUp)
            {
                RemoveGraphicsAndCollider();
                StartCoroutine(HexDamage(playerStats, other));
            }
            else if(gameObject.tag == "Ammo")
            {
                AmmoPack(weapon);
            } else if(gameObject.tag == "Bomb")
            {
                BombPack(weapon);
            }
        }
    }

    private void HpPack(PlayerStats player)
    {
        if(player.Hp < player.MaxHp) //don't pick up unless player is actually healed
        {
            player.HealPlayer(healAmount);
            Destroy(gameObject);
        }
        Debug.Log(player.Hp);
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

    private IEnumerator HexDamage(PlayerStats player, Collider2D playerCollider)
    {
        isHexDamageOn = true;
        player.IsHexDamageUp = true;
        while (isHexDamageOn)
        {
            PlayerController pc = playerCollider.GetComponent<PlayerController>();

            pc.ChangePlayerColor(pc.HexDamageColor); 
            int originalDamageMultiplier = player.DamageMultiplier;
            player.DamageMultiplier = 6;
            yield return new WaitForSeconds(hexDamageDuration);
            
            player.DamageMultiplier = originalDamageMultiplier;
            isHexDamageOn = false;
            player.IsHexDamageUp = false;
            pc.ChangePlayerColor(pc.OriginalColor);
            Destroy(gameObject);
        }

    }

    public void RemoveGraphicsAndCollider()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
    */
}
