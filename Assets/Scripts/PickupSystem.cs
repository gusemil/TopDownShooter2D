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
    private const float pickupDropChance = 5f;

    public List<GameObject> PickupList { get { return pickupList; } set { pickupList = value; } }

    // Start is called before the first frame update
    void Start()
    {
        pickupList.Add(hexDamagePreFab);
        pickupList.Add(infiniteAmmoPreFab);
        pickupList.Add(ammoPreFab);
        pickupList.Add(bombPreFab);
        pickupList.Add(pointMultiplierPreFab);
        pickupList.Add(shieldPreFab);
        pickupList.Add(infiniteDashPreFab);
        pickupList.Add(godModePreFab);
    }

    public void SpawnPickUpFromEnemy(GameObject enemy)
    {
        if(Random.Range(0,99) < pickupDropChance)
        {
            Instantiate(pickupList[Random.Range(0, pickupList.Count)], enemy.transform.position, Quaternion.identity);
        }
    }

    //TO DO randomoi chancet eri pickuppeihin itsessään. Suunnittele notepadilla
    /*
    private void SelectPickupToDrop()
    {

    }
    */
}
