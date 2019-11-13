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

    /*
    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }
    */
    public PlayerStats()
    {
        hp = 100;
        maxHp = 100;
    }

        // Start is called before the first frame update
        void Start()
    {
        //hp = 100;
        //maxHp = 100;
        //damage = 10;
    }

}
