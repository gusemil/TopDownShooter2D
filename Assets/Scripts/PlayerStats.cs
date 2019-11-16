using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats
{
    private int hp;
    private int maxHp;
    private int damage;

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


    public PlayerStats()
    {
        hp = 50;
        maxHp = 100;
        Damage = 20;
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
        hp -= dmg;
        Debug.Log("Player takes " + dmg + " damage. Hp left: " + hp);
    }
}
