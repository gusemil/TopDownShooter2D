using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSplatter : MonoBehaviour
{
    private float bloodSplatterTimeToDestroy = 10f;
    private SpriteRenderer sr;
    private float bloodTimer;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        bloodTimer = bloodSplatterTimeToDestroy;
        //sr.color = new Color(1f, 1f, 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        bloodTimer -= Time.deltaTime;
        sr.color = new Color(1f, 1f, 1f, bloodTimer / 10);
        Destroy(gameObject, bloodSplatterTimeToDestroy);
    }
}
