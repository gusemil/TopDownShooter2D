using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSystem : MonoBehaviour
{
    public GameObject hexDamagePreFab;
    public GameObject infiniteAmmoPreFab;
    public GameObject ammoPreFab;
    public GameObject bombPreFab;
    public GameObject pointMultiplierPreFab;
    public GameObject shieldPreFab;
    public GameObject infiniteDashPreFab;
    public GameObject godModePreFab;

    private static List<GameObject> pickupList = new List<GameObject>();
    private const float pickupDropChance = 10f;
    private GameManager gm;

    public List<GameObject> PickupList { get { return pickupList; } set { pickupList = value; } }

    // Start is called before the first frame update
    void Start()
    {
        pickupList.Add(pointMultiplierPreFab);
        pickupList.Add(ammoPreFab);
        pickupList.Add(hexDamagePreFab);
        pickupList.Add(infiniteDashPreFab);
        pickupList.Add(bombPreFab);
        pickupList.Add(shieldPreFab);
        pickupList.Add(infiniteAmmoPreFab);
        pickupList.Add(godModePreFab);

        gm = GameManager.instance;
    }

    public void SpawnPickUpFromEnemy(GameObject enemy)
    {
        if(Random.Range(0,99) < pickupDropChance)
        {
            RandomPickupToDrop(enemy);
        }
    }

    private void RandomPickupToDrop(GameObject enemy)
    {
        float random = Random.Range(0, 99);

        if(random < 30)
        {
            Instantiate(pickupList[0], enemy.transform.position, Quaternion.identity); //point
        } else if (random >= 30 && random < 60)
        {
            Instantiate(pickupList[1], enemy.transform.position, Quaternion.identity); //ammo
        } else if (random >= 60 && random < 70)
        {
            Instantiate(pickupList[2], enemy.transform.position, Quaternion.identity); //hexDamage
        } else if(random >= 70 && random < 77.5)
        {
            Instantiate(pickupList[3], enemy.transform.position, Quaternion.identity); //infiniteDash
        } else if (random >= 77.5 && random < 85)
        {
            Instantiate(pickupList[4], enemy.transform.position, Quaternion.identity); //bomb
        } else if (random >= 85 && random < 92.5)
        {
            Instantiate(pickupList[5], enemy.transform.position, Quaternion.identity); //shield
        } else if (random >= 92.5 && random < 97.5)
        {
                Instantiate(pickupList[6], enemy.transform.position, Quaternion.identity); //infiniteAmmo

        } else if (random >= 97.5 && random < 100)
        {
                Instantiate(pickupList[7], enemy.transform.position, Quaternion.identity); //GodMode
        }
    }
}
