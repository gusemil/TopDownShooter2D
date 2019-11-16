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
            Color originalColor = pc.OriginalColor;

            pc.ChangePlayerColor(new Color(0, 1, 1, 1)); 
            int originalDamage = player.Damage;
            player.Damage *= 6;
            yield return new WaitForSeconds(hexDamageDuration);
            
            player.Damage = originalDamage;
            pc.ChangePlayerColor(originalColor);
            isHexDamageOn = false;
            player.IsPoweredUp = false;
            Destroy(gameObject);
        }

        /*
        if (!isHexDamageOn)
        {
            Destroy(gameObject);
        }
        */

    }

    /*
    private void ChangePlayerColor(Color color, Collider2D playerCollider)
    {
        playerCollider.GetComponent<SpriteRenderer>().color = color;
    }
    */
}
