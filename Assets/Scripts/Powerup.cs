using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : Pickup
{
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
            PlayerStats playerStats = GameManager.status.PlayerStats;
            if (gameObject.tag == "HpPack")
            {
                HpPack(playerStats);
            }
            else if (gameObject.tag == "HexDamage" && !playerStats.IsPoweredUp)
            {
                RemoveGraphicsAndCollider();
                StartCoroutine(HexDamage(playerStats, other));
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

    private IEnumerator HexDamage(PlayerStats player, Collider2D playerCollider)
    {
        isHexDamageOn = true;
        player.IsPoweredUp = true;
        while (isHexDamageOn)
        {
            PlayerController pc = playerCollider.GetComponent<PlayerController>();

            pc.ChangePlayerColor(pc.PowerUpColor); 
            int originalDamageMultiplier = player.DamageMultiplier;
            player.DamageMultiplier = 6;
            yield return new WaitForSeconds(hexDamageDuration);
            
            player.DamageMultiplier = originalDamageMultiplier;
            isHexDamageOn = false;
            player.IsPoweredUp = false;
            pc.SetPreviousColor();
            Destroy(gameObject);
        }

    }

}
