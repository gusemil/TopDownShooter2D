using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : Pickup
{
    private float PowerUpDuration = 5f;
    private bool isPowerUpOn;
    private int healAmount = 20;

    public bool IsPowerUpOn
    {
        get { return isPowerUpOn; }
        set { isPowerUpOn = value; }
    }

    private void Awake()
    {
        isPowerUpOn = false;
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
            else if (gameObject.tag == "HexDamage")
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
        isPowerUpOn = true;
        Debug.Log("HexDamage");
        while (isPowerUpOn)
        {
            PlayerController pc = playerCollider.GetComponent<PlayerController>();
            Color originalColor = pc.OriginalColor;

            pc.ChangePlayerColor(new Color(0, 1, 1, 1)); 
            int originalDamage = player.Damage;
            player.Damage *= 6;
            Debug.Log("PowerUpIsOn = true");
            yield return new WaitForSeconds(PowerUpDuration);
            isPowerUpOn = false;
            player.Damage = originalDamage;

            pc.ChangePlayerColor(originalColor); //return color to original

        }

        if (!isPowerUpOn)
        {
            Destroy(gameObject);
        }

    }

    /*
    private void ChangePlayerColor(Color color, Collider2D playerCollider)
    {
        playerCollider.GetComponent<SpriteRenderer>().color = color;
    }
    */
}
