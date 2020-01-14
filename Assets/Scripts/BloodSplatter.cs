using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSplatter : MonoBehaviour
{
    private float bloodSplatterTimeToDestroy = 10f;
    private SpriteRenderer sr;
    private float bloodTimer;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        bloodTimer = bloodSplatterTimeToDestroy;
    }

    void Update()
    {
        bloodTimer -= Time.deltaTime;
        sr.color = new Color(1f, 1f, 1f, bloodTimer / 10);
        Destroy(gameObject, bloodSplatterTimeToDestroy);
    }
}
