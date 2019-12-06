using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private float bulletForce; //20f;
    private float fireCoolDown; //0.1f;
    private int weaponNumber; //0 machine gun, 1 shotgun
    private string weaponName;
    private float weaponTimer;

    // Start is called before the first frame update
    void Start()
    {
        //weaponNumber = 0;
        weaponTimer += Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
