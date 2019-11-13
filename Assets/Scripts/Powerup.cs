using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : Pickup
{
    private float PowerUpDuration = 5f;
    private bool isPowerUpOn;

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
                Destroy(gameObject);
            }
            else if (gameObject.tag == "HexDamage")
            {
                RemoveGraphicsAndCollider();
                StartCoroutine(HexDamage(playerStats));
            }
        }
    }

    private void HpPack(PlayerStats playerObject)
    {
        //playerObject.TakeDamage(20);
        playerObject.Hp -= 20;
        Debug.Log(playerObject.Hp);
    }

    private IEnumerator HexDamage(PlayerStats playerObject)
    {
        isPowerUpOn = true;
        Debug.Log("HexDamage");
        while (isPowerUpOn)
        {
            int originalHp = playerObject.Hp;
            playerObject.Hp = 200;
            Debug.Log("PowerUpIsOn = true");
            yield return new WaitForSeconds(PowerUpDuration);
            isPowerUpOn = false;
            playerObject.Hp = originalHp;
        }

        if (!isPowerUpOn)
        {
            Destroy(gameObject);
        }

    }
}
