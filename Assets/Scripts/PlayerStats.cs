using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats
{
    private int hp;
    private int maxHp;
    private int damage;
    private bool isPoweredUp;
    private bool isDashing;

    public int Hp
    {
        get { return hp; }
        set { hp = value; }
    }
    public int MaxHp
    {
        get { return maxHp; }
        set { maxHp = value; }
    }


    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }

    public bool IsPoweredUp
    {
        get { return isPoweredUp; }
        set { isPoweredUp = value; }
    }

    public bool IsDashing
    {
        get { return isDashing; }
        set { isDashing = value; }
    }

    public PlayerStats()
    {
        hp = 50;
        maxHp = 100;
        damage = 20;
        isPoweredUp = false;
        isDashing = false;
    }


    public void HealPlayer(int healAmount)
    {
        if (Hp < MaxHp)
        {
            if ((Hp + healAmount) > MaxHp)
            {
                Hp = MaxHp;
            }
            else
            {
                Hp += healAmount;
            }
        }
    }

    public void TakeDamage(int dmg)
    {
        if (!isDashing) //don't take damage if player is dashing
        {
            hp -= dmg;
            Debug.Log("Player takes " + dmg + " damage. Hp left: " + hp);

            if(hp <= 0)
            {
                GameManager.status.IsGameOver = true;
                GameManager.status.Pause.TogglePause();
            }
        }
    }

    /*
    [Serializable]
    class PlayerData
    {
        public float health;
        public float maxHealth;
        public string currentLevel;
    }
    */
}
